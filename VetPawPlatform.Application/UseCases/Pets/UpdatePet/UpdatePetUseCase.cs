using VetPawPlatform.Application.Dto;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Application.UseCases.Pets.UpdatePet;

public class UpdatePetUseCase(IPetRepository repository)
{
    public async Task<PetResponseDto?> ExecuteAsync(Guid id, UpdatePetDto dto)
    {        
        var pet = await repository.GetByIdAsync(id);

        if (pet == null) return null;
     
        pet.Name = dto.Name;
        pet.Species = dto.Species;
        pet.BirthDate = dto.BirthDate;
        
        await repository.UpdateAsync(pet);
        
        return pet;
    }
}