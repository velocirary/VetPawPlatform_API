using VetPawPlatform.Domain.ValueObjects;
using VetPawPlatform.Domain.Exceptions;
using VetPawPlatform.Domain.Enums;

namespace VetPawPlatform.Domain.Entities;

public class Owner
{
    public Guid Id { get; private set; }
    public Cpf Document { get; private set; } = null!;
    public Name FullName { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public Phone PhoneNumber { get; private set; } = null!;
    public DateTime BirthDate { get; private set; }

    private readonly List<Pet> _pets = [];
    public IReadOnlyCollection<Pet> Pets => _pets;

    public Owner(Cpf document, Name fullName, Email email, Phone phoneNumber, DateTime birthDate)
    {
        ValidateAge(birthDate);

        Id = Guid.NewGuid();
        Document = document;
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        BirthDate = birthDate;
    }

    public void UpdateDetails(Cpf document, Name fullName, Email email, Phone phoneNumber, DateTime birthDate)
    {
        ValidateAge(birthDate);

        Document = document;
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        BirthDate = birthDate;
    }

    private static void ValidateAge(DateTime birthDate)
    {
        if (birthDate > DateTime.UtcNow.AddYears(-18))
            throw new DomainException("O tutor deve ser maior de 18 anos.");
    }

    public void AddPet(Pet pet)
    {
        if (pet == null)
            throw new DomainException("O pet não pode ser nulo.");

        if (pet.OwnerId != Id)
            throw new DomainException("Este pet não pertence a este tutor.");

        if (_pets.Any(pet => pet.Id == pet.Id))
            return;

        _pets.Add(pet);
    }

    public void UpdatePet(Guid petId, Name name, PetSpecies species, DateTime birthDate)
    {
        var pet = _pets.FirstOrDefault(p => p.Id == petId) 
            ?? throw new DomainException("Pet não encontrado neste tutor.");

        pet.UpdateDetails(name, species, birthDate);
    }

    public void RemovePet(Guid petId)
    {
        var pet = _pets.FirstOrDefault(pet => pet.Id == petId)
            ?? throw new DomainException("Pet não encontrado para este tutor.");

        _pets.Remove(pet);
    }

    public static Owner Rehydrate(Guid id, Cpf document, Name fullName, Email email, Phone phoneNumber, DateTime birthDate, IEnumerable<Pet>? pets = null)
    {
        var owner = new Owner
        {
            Id = id,
            Document = document,
            FullName = fullName,
            Email = email,
            PhoneNumber = phoneNumber,
            BirthDate = birthDate
        };

        if (pets != null)
            foreach (var pet in pets)
                owner.AddPet(pet);

        return owner;
    }

    private Owner() { }
}