namespace VetPawPlatform.Infra.DynamoDB.Models;

using Amazon.DynamoDBv2.DataModel;

[DynamoDBTable("Users")]
public class UserDbModel
{
    [DynamoDBHashKey]
    public string Id { get; set; } = null!;

    [DynamoDBGlobalSecondaryIndexHashKey("Email-index")]
    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;
    public int Role { get; set; }
}