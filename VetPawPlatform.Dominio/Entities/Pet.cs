using Amazon.DynamoDBv2.DataModel;
using VetPawPlatform.Domain.Enums;

namespace VetPawPlatform.Domain.Entities;

[DynamoDBTable("Pets")]
public class Pet
{
    [DynamoDBHashKey]
    public Guid Id { get; set; }

    [DynamoDBProperty]
    public string Name { get; set; } = string.Empty;

    [DynamoDBProperty]
    public PetSpecies Species { get; set; } = PetSpecies.None;

    [DynamoDBProperty]
    public DateTime BirthDate { get; set; }
   
    public Pet(string name, PetSpecies species, DateTime birthDate)
    {
        Id = Guid.NewGuid();
        Name = name;
        Species = species;
        BirthDate = birthDate;
    }

    public Pet() { }
}