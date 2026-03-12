using VetPawPlatform.Application.Dto.Pets;
using VetPawPlatform.Application.Mappings;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Application.UseCases.Pets.GetAllPet;

public class GetAllPetsUseCase(IPetRepository repository)
{
    public async Task<IEnumerable<PetResponseDto>> ExecuteAsync()
    {
        var pets = await repository.GetAllAsync();

        return pets.Select(pet => pet.ToResponse());
    }
}