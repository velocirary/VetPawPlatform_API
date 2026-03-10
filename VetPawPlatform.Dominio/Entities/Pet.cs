using VetPawPlatform.Domain.Enums;
using VetPawPlatform.Domain.Exceptions;

namespace VetPawPlatform.Domain.Entities;
public class Pet
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public PetSpecies Species { get; private set; }
    public DateTime BirthDate { get; private set; }
    
    public Pet(string name, PetSpecies species, DateTime birthDate)
    {
        Validate(name, birthDate, species);
        Id = Guid.NewGuid();
        Name = name;
        Species = species;
        BirthDate = birthDate;
    }
    
    public void UpdateDetails(string name, PetSpecies species, DateTime birthDate)
    {
        Validate(name, birthDate, species);
        Name = name;
        Species = species;
        BirthDate = birthDate;
    }
    
    public static Pet Rehydrate(Guid id, string name, PetSpecies species, DateTime birthDate)
    {
        return new Pet 
        { 
            Id = id, 
            Name = name, 
            Species = species, 
            BirthDate = birthDate 
        };
    }

    private static void Validate(string name, DateTime birthDate, PetSpecies species)
    {
        if (string.IsNullOrWhiteSpace(name) || name == "string")
            throw new DomainException("Nome é obrigatório");

        if (birthDate > DateTime.UtcNow.Date)
            throw new DomainException("A data de nascimento não pode ser maior que hoje.");

        if (!Enum.IsDefined(typeof(PetSpecies), species) || species == PetSpecies.None)
            throw new DomainException("Espécie do pet inválida.");
    }

    private Pet() { }
}