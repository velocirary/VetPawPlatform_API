using Microsoft.Extensions.DependencyInjection;
using VetPawPlatform.Application.UseCases.Owner.GetOwnerById;
using VetPawPlatform.Application.UseCases.Pets.CreatePet;
using VetPawPlatform.Application.UseCases.Pets.GetAllPet;
using VetPawPlatform.Application.UseCases.Pets.GetPetById;
using VetPawPlatform.Application.UseCases.Pets.UpdatePet;

namespace VetPawPlatform.Application.DependencyInjection;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreatePetUseCase>();
        services.AddScoped<GetPetByIdUseCase>();        
        services.AddScoped<GetAllPetsUseCase>();
        services.AddScoped<UpdatePetUseCase>();
        services.AddScoped<CreateOwnerUseCase>();
        services.AddScoped<GetOwnerByIdUseCase>();

        return services;
    }
}