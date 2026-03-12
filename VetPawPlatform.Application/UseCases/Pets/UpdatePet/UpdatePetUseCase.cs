using VetPawPlatform.Application.Dto.Pets;
using VetPawPlatform.Application.Mappings;
using VetPawPlatform.Domain.Exceptions;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Application.UseCases.Pets.UpdatePet;

public class UpdatePetUseCase(IOwnerRepository repository)
{
    public async Task<PetResponseDto?> ExecuteAsync(Guid id, UpdatePetDto dto)
    {
        var petReference = await repository.GetPetByIdAsync(id) 
                        ?? throw new DomainException($"Pet {id} não encontrado.");

        var owner = await repository.GetByIdAsync(petReference.OwnerId);

        if (owner == null) 
            return null;

        owner.UpdatePet(id, dto.Name, dto.Species, dto.BirthDate);

        await repository.UpdateAsync(owner);

        var updatedPet = owner.Pets.First(pet => pet.Id == id);
        return updatedPet.ToResponse();
    }
}