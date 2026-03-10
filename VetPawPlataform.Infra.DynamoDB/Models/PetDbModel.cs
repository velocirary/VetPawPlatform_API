using Amazon.DynamoDBv2.DataModel;

namespace VetPawPlatform.Infrastructure.Models;

[DynamoDBTable("Pets")]
public class PetDbModel
{
    [DynamoDBHashKey]
    public Guid Id { get; set; }

    [DynamoDBProperty]
    public string Name { get; set; } = string.Empty;

    [DynamoDBProperty]
    public int Species { get; set; }

    [DynamoDBProperty]
    public string BirthDate { get; set; } = string.Empty;
}