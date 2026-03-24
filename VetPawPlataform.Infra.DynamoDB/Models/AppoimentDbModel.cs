using Amazon.DynamoDBv2.DataModel;

namespace VetPawPlatform.Infra.DynamoDB.Models;

[DynamoDBTable("Appointments")]
public class AppointmentDbModel
{
    [DynamoDBHashKey]
    public Guid Id { get; set; }

    [DynamoDBProperty]
    public Guid OwnerId { get; set; }

    [DynamoDBProperty]
    public Guid PetId { get; set; }

    [DynamoDBProperty]
    public DateTime Date { get; set; }

    [DynamoDBProperty]
    public string Reason { get; set; } = string.Empty;

    [DynamoDBProperty]
    public string Status { get; set; } = string.Empty;

    [DynamoDBProperty]
    public string Notes { get; set; } = string.Empty;

    [DynamoDBProperty]
    public DateTime CreatedAt { get; set; }

    [DynamoDBProperty]
    public DateTime? UpdatedAt { get; set; }
}