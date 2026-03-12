using VetPawPlatform.Application.Dto.Owners;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Application.UseCases.Owners.GetAllOwner;

public class GetAllOwnerUseCase(IOwnerRepository repository)
{
    public async Task<IEnumerable<OwnerResponseDto>> ExecuteAsync()
    {
        var owner = await repository.GetAllAsync();

        return owner.Select(owner => (OwnerResponseDto)owner);
    }
}