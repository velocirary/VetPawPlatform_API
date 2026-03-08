using Amazon.DynamoDBv2.DataModel;

namespace VetPawPlatform.Domain.Entities;

[DynamoDBTable("Pets")]
public class Pet
{
    [DynamoDBHashKey]
    public Guid Id { get; set; }

    [DynamoDBProperty]
    public string Name { get; set; } = string.Empty;

    [DynamoDBProperty]
    public string Species { get; set; } = string.Empty;

    [DynamoDBProperty]
    public DateTime BirthDate { get; set; }
   
    public Pet(string name, string species, DateTime birthDate)
    {
        Id = Guid.NewGuid();
        Name = name;
        Species = species;
        BirthDate = birthDate;
    }

    public Pet() { }
}