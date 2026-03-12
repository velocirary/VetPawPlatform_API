using VetPawPlatform.Application.Dto.Pets;
using VetPawPlatform.Application.Mappings;
using VetPawPlatform.Domain.Exceptions;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Application.UseCases.Pets.GetPetById;

public class GetPetByIdUseCase(IOwnerRepository repository)
{
    public async Task<PetResponseDto?> ExecuteAsync(Guid id)
    {
        var pet = await repository.GetPetByIdAsync(id);

        if (pet == null)
            throw new DomainException($"Pet com o id: '{id}' não encontrado");

        return pet.ToResponse();
    }
}