using VetPawPlatform.Application.Dto.Pets;
using VetPawPlatform.Application.Mappings;
using VetPawPlatform.Domain.Entities;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Application.UseCases.Pets.CreatePet;

public class CreatePetUseCase(IPetRepository repository)
{
    public async Task<PetResponseDto> ExecuteAsync(CreatePetDto dto)
    {
        var pet = new Pet(
            dto.OwnerId,
            dto.Name,
            dto.Species,
            dto.BirthDate
        );
        
        await repository.CreateAsync(pet);

        return pet.ToResponse();
    }
}