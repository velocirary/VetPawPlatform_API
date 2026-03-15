using VetPawPlatform.Application.Dto.Owners;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Application.UseCases.Owners.GetAllOwner;

public class GetAllOwnerUseCase(IOwnerRepository repository)
{
    public async Task<IEnumerable<OwnerResponseDto>> ExecuteAsync()
    {
        var owners = await repository.GetAllAsync();

        return owners.Select(owner => (OwnerResponseDto)owner);
    }
}