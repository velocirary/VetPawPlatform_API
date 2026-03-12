using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VetPawPlatform.Domain.Interfaces;
using VetPawPlatform.Infra.Repositories;

namespace VetPawPlatform.Infra.DynamoDB.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var useLocal = configuration.GetValue<bool>("UseLocalDynamo");
        var serviceUrl = configuration.GetValue<string>("DynamoDbServiceUrl");

        var region = configuration["AWS:Region"];

        var config = new AmazonDynamoDBConfig
        {
            ServiceURL = serviceUrl,
            UseHttp = true,
            AuthenticationRegion = region
        };

        var credentials = new BasicAWSCredentials("local", "local");

        services.AddSingleton<IAmazonDynamoDB>(new AmazonDynamoDBClient(credentials, config));

        services.AddSingleton<IDynamoDBContext, DynamoDBContext>();        
        services.AddScoped<IOwnerRepository, OwnerRepository>();
        services.AddSingleton<DynamoDbInitializer>();

        return services;
    }
}