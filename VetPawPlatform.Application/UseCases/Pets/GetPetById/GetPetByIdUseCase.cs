using VetPawPlatform.Domain.Exceptions;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Application.UseCases.Pets.GetPetById;

public class GetPetByIdUseCase(IPetRepository repository)
{    
    public async Task<PetResponseDto?> ExecuteAsync(Guid id)
    {
        var pet = await repository.GetByIdAsync(id);

        if (pet == null)
            throw new NotFoundException($"Pet com o id: '{id}' não encontrado");

        return pet;
    }
}