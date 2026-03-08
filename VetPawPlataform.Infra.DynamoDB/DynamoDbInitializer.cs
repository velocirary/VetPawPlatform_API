using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace VetPawPlatform.Infra.DynamoDB;

public class DynamoDbInitializer(IAmazonDynamoDB dynamoDb)
{
    public async Task InitializeAsync()
    {
        var tableName = "Pets";
        
        var tables = await dynamoDb.ListTablesAsync();

        if (tables.TableNames.Contains(tableName))
            return;
        
        var request = new CreateTableRequest
        {
            TableName = tableName,
            AttributeDefinitions =
            [
                new() {
                    AttributeName = "Id",
                    AttributeType = ScalarAttributeType.S
                }
            ],
            KeySchema =
            [
                new() {
                    AttributeName = "Id",
                    KeyType = KeyType.HASH
                }
            ],
            ProvisionedThroughput = new ProvisionedThroughput
            {
                ReadCapacityUnits = 5,
                WriteCapacityUnits = 5
            }
        };
        
        await dynamoDb.CreateTableAsync(request);
    }
}