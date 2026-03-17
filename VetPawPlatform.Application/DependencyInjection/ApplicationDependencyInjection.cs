using Microsoft.Extensions.DependencyInjection;
using VetPawPlatform.Application.UseCases.Owners.AddPetToOwner;
using VetPawPlatform.Application.UseCases.Owners.CreateOwner;
using VetPawPlatform.Application.UseCases.Owners.GetAllOwner;
using VetPawPlatform.Application.UseCases.Owners.GetOwnerById;
using VetPawPlatform.Application.UseCases.Owners.RemovePetFromOwner;
using VetPawPlatform.Application.UseCases.Owners.UpdateOwner;
using VetPawPlatform.Application.UseCases.Pets.GetAllPet;
using VetPawPlatform.Application.UseCases.Pets.GetPetById;
using VetPawPlatform.Application.UseCases.Pets.UpdatePet;

namespace VetPawPlatform.Application.DependencyInjection;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<GetPetByIdUseCase>();        
        services.AddScoped<GetAllPetsUseCase>();
        services.AddScoped<UpdatePetUseCase>();
        services.AddScoped<CreateOwnerUseCase>();
        services.AddScoped<GetOwnerByIdUseCase>();
        services.AddScoped<GetAllOwnerUseCase>();
        services.AddScoped<UpdateOwnerUseCase>();
        services.AddScoped<AddPetToOwnerUseCase>();
        services.AddScoped<RemovePetFromOwnerUseCase>();

        return services;
    }
}