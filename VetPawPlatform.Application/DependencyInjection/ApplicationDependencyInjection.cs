using Microsoft.Extensions.DependencyInjection;
using VetPawPlatform.Application.UseCases.Pets.GetAllPet;
using VetPawPlatform.Application.UseCases.Pets.GetPetById;

namespace VetPawPlatform.Application.DependencyInjection;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreatePetUseCase>();
        services.AddScoped<GetPetByIdUseCase>();
        services.AddScoped<GetAllPetsUseCase>();

        return services;
    }
}