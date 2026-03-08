using VetPawPlatform.Application.Dto;
using VetPawPlatform.Domain.Entities;
using VetPawPlatform.Domain.Interfaces;

public class CreatePetUseCase(IPetRepository repository)
{
    public async Task<PetResponseDto> ExecuteAsync(CreatePetDto dto)
    {
        var pet = new Pet(
            dto.Name,
            dto.Species,
            dto.BirthDate
        );
        
        await repository.CreateAsync(pet);
        
        return pet;
    }
}