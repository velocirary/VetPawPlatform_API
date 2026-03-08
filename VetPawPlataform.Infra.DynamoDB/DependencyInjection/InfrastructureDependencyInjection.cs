using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VetPawPlatform.Domain.Interfaces;
using VetPawPlatform.Infra.DynamoDB.Repositories;

namespace VetPawPlatform.Infra.DynamoDB.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var awsOptions = configuration.GetAWSOptions();

        if (configuration.GetValue<bool>("UseLocalDynamo"))
        {
            _ = configuration.GetValue<string>("DynamoDbServiceUrl");
            awsOptions.DefaultClientConfig.UseHttp = true;
        }

        services.AddDefaultAWSOptions(awsOptions);
        services.AddAWSService<IAmazonDynamoDB>();
        services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
        services.AddScoped<IPetRepository, PetRepository>();
        services.AddSingleton<DynamoDbInitializer>();

        return services;
    }
}