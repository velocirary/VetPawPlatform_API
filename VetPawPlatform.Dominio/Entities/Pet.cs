using VetPawPlatform.Domain.Enums;
using VetPawPlatform.Domain.Exceptions;
using VetPawPlatform.Domain.ValueObjects;

public class Pet
{
    public Guid Id { get; private set; }
    public Guid OwnerId { get; private set; }
    public Name Name { get; private set; } = null!;
    public PetSpecies Species { get; private set; }
    public DateTime BirthDate { get; private set; }

    public Pet(Guid ownerId, Name name, PetSpecies species, DateTime birthDate)
    {
        ValidateBusinessRules(ownerId, birthDate, species);

        Id = Guid.NewGuid();
        OwnerId = ownerId;
        Name = name;
        Species = species;
        BirthDate = birthDate;
    }

    public void UpdateDetails(Name name, PetSpecies species, DateTime birthDate)
    {
        ValidateBusinessRules(this.OwnerId, birthDate, species);
        Name = name;
        Species = species;
        BirthDate = birthDate;
    }

    public static Pet Rehydrate(Guid id, Guid ownerId, Name name, PetSpecies species, DateTime birthDate)
    {
        return new Pet
        {
            Id = id,
            OwnerId = ownerId,
            Name = name,
            Species = species,
            BirthDate = birthDate
        };
    }

    private static void ValidateBusinessRules(Guid ownerId, DateTime birthDate, PetSpecies species)
    {
        if (ownerId == Guid.Empty)
            throw new DomainException("O Pet deve obrigatoriamente ter um dono.");

        if (birthDate > DateTime.UtcNow.Date)
            throw new DomainException("A data de nascimento não pode ser maior que hoje.");

        if (!Enum.IsDefined(typeof(PetSpecies), species) || species == PetSpecies.None)
            throw new DomainException("Espécie do pet inválida.");
    }

    private Pet() { }
}