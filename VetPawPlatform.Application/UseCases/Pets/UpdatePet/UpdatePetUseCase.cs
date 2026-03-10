using VetPawPlatform.Application.Dto;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Application.UseCases.Pets.UpdatePet;

public class UpdatePetUseCase(IPetRepository repository)
{
    public async Task<PetResponseDto?> ExecuteAsync(Guid id, UpdatePetDto dto)
    {
        var pet = await repository.GetByIdAsync(id);

        if (pet == null) 
            return null;

        pet.UpdateDetails(
            dto.Name, 
            dto.Species, 
            dto.BirthDate);

        await repository.UpdateAsync(pet);

        return pet;
    }
}