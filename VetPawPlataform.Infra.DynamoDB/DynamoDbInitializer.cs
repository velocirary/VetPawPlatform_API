using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace VetPawPlatform.Infra.DynamoDB;

public class DynamoDbInitializer(IAmazonDynamoDB dynamoDb)
{
    public async Task InitializeAsync()
    {
        await CreateTableOwnersAsync();
    }

    private async Task CreateTableOwnersAsync()
    {
        var tableName = "Owners";
        var tables = await dynamoDb.ListTablesAsync();

        if (tables.TableNames.Contains(tableName))
            return;

        var request = new CreateTableRequest
        {
            TableName = tableName,
            AttributeDefinitions =
            [
                new() { AttributeName = "Id", AttributeType = ScalarAttributeType.S },
                new() { AttributeName = "Document", AttributeType = ScalarAttributeType.S }
            ],
            KeySchema =
            [
                new() { AttributeName = "Id", KeyType = KeyType.HASH }
            ],
            GlobalSecondaryIndexes =
            [
                new GlobalSecondaryIndex
                {
                    IndexName = "Document-Index",
                    KeySchema =
                    [
                        new KeySchemaElement { AttributeName = "Document", KeyType = KeyType.HASH }
                    ],
                    Projection = new Projection { ProjectionType = ProjectionType.ALL },
                    ProvisionedThroughput = new ProvisionedThroughput { ReadCapacityUnits = 5, WriteCapacityUnits = 5 }
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