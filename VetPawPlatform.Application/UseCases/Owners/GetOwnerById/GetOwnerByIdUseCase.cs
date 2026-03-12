using VetPawPlatform.Application.Dto.Owners;
using VetPawPlatform.Domain.Exceptions;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Application.UseCases.Owners.GetOwnerById;

public class GetOwnerByIdUseCase(IOwnerRepository repository)
{    
    public async Task<OwnerResponseDto?> ExecuteAsync(Guid id)
    {
        var owner = await repository.GetByIdAsync(id);

        return owner == null ? throw new NotFoundException($"Proprietário com o id: '{id}' não encontrado") : (OwnerResponseDto)owner;
    }
}