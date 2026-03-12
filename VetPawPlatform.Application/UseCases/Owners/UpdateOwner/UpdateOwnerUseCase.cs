using VetPawPlatform.Application.Dto.Owners;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Application.UseCases.Owners.UpdateOwner;

public class UpdateOwnerUseCase(IOwnerRepository repository)
{
    public async Task<OwnerResponseDto?> ExecuteAsync(Guid id, UpdateOwnerDto dto)
    {
        var owner = await repository.GetByIdAsync(id);

        if (owner == null) 
            return null;

        owner.UpdateDetails(
            dto.Document, 
            dto.FullName, 
            dto.Email,
            dto.PhoneNumber,
            dto.BirthDate
            );

        await repository.UpdateAsync(owner);

        return owner;
    }
}