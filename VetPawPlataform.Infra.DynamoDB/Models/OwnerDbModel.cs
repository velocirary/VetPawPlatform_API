using Amazon.DynamoDBv2.DataModel;

namespace VetPawPlatform.Infra.Models;

[DynamoDBTable("Owners")]
public class OwnerDbModel
{
    [DynamoDBHashKey]
    public Guid Id { get; set; }

    [DynamoDBProperty]
    public string FullName { get; set; } = string.Empty;

    [DynamoDBProperty]
    public string Email { get; set; } = string.Empty;

    [DynamoDBProperty]
    public string Document { get; set; } = string.Empty;

    [DynamoDBProperty]
    public string PhoneNumber { get; set; } = string.Empty;

    [DynamoDBProperty]
    public string BirthDate { get; set; } = string.Empty;

    [DynamoDBProperty]
    public List<PetDbModel> Pets { get; set; } = [];
}