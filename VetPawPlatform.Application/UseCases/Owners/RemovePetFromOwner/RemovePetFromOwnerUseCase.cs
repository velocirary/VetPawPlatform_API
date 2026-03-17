using VetPawPlatform.Domain.Exceptions;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Application.UseCases.Owners.RemovePetFromOwner;

public class RemovePetFromOwnerUseCase(IOwnerRepository repository)
{
    public async Task ExecuteAsync(Guid ownerId, Guid petId)
    {
        var owner = await repository.GetByIdAsync(ownerId)
            ?? throw new DomainException("Tutor não encontrado.");

        owner.RemovePet(petId);

        await repository.UpdateAsync(owner);
    }
}