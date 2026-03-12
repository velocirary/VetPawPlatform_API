using VetPawPlatform.Domain.Enums;
using VetPawPlatform.Domain.Exceptions;

namespace VetPawPlatform.Domain.Entities;

public class Pet
{
    public Guid Id { get; private set; }
    public Guid OwnerId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public PetSpecies Species { get; private set; }
    public DateTime BirthDate { get; private set; }
    
    public Pet(Guid ownerId, string name, PetSpecies species, DateTime birthDate)
    {
        Validate(ownerId, name, birthDate, species);

        Id = Guid.NewGuid();
        OwnerId = ownerId;
        Name = name;
        Species = species;
        BirthDate = birthDate;
    }
    
    public void UpdateDetails(string name, PetSpecies species, DateTime birthDate)
    {
        Validate(this.OwnerId, name, birthDate, species);
        Name = name;
        Species = species;
        BirthDate = birthDate;
    }
    
    public static Pet Rehydrate(Guid id, Guid ownerId, string name, PetSpecies species, DateTime birthDate)
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

    private static void Validate(Guid ownerId, string name, DateTime birthDate, PetSpecies species)
    {
        if (ownerId == Guid.Empty)
            throw new DomainException("O Pet deve obrigatoriamente ter um dono.");

        if (string.IsNullOrWhiteSpace(name) || name == "string")
            throw new DomainException("Nome é obrigatório");

        if (birthDate > DateTime.UtcNow.Date)
            throw new DomainException("A data de nascimento não pode ser maior que hoje.");

        if (!Enum.IsDefined(typeof(PetSpecies), species) || species == PetSpecies.None)
            throw new DomainException("Espécie do pet inválida.");
    }

    private Pet() { }
}