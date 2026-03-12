using VetPawPlatform.Application.Dto.Pets;
using VetPawPlatform.Domain.Entities;
using VetPawPlatform.Domain.Exceptions;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Application.UseCases.Owners.AddPetToOwner;

public class AddPetToOwnerUseCase(IOwnerRepository repository)
{
    public async Task<PetResponseDto> ExecuteAsync(Guid ownerId, CreatePetDto dto)
    {
        var owner = await repository.GetByIdAsync(ownerId) 
                ?? throw new DomainException("...");

        var pet = new Pet(
            ownerId,
            dto.Name,
            dto.Species,
            dto.BirthDate);

        owner.AddPet(pet);
        await repository.UpdateAsync(owner);

        return new PetResponseDto(pet.Id, pet.OwnerId, pet.Name, pet.Species, pet.BirthDate);
    }
}