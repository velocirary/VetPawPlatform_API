using Microsoft.Extensions.DependencyInjection;
using VetPawPlatform.Application.Common.Security;
using VetPawPlatform.Infra.Security;
using IPasswordHasher = VetPawPlatform.Application.Common.Security.IPasswordHasher;
using PasswordHasher = VetPawPlatform.Infra.Security.PasswordHasher;

public static class DependencyInjection
{
    public static IServiceCollection AddSecurity(this IServiceCollection services, string jwtSecret)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtService>(sp => new JwtService(jwtSecret));        

        return services;
    }
}