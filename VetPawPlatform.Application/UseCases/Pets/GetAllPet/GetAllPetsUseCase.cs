using VetPawPlatform.Application.Dto.Pets;
using VetPawPlatform.Application.Mappings;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Application.UseCases.Pets.GetAllPet;

public class GetAllPetsUseCase(IOwnerRepository repository)
{
    public async Task<IEnumerable<PetResponseDto>> ExecuteAsync()
    {
        var pets = await repository.GetAllPetsAsync();

        return pets.Select(pet => pet.ToResponse());
    }
}