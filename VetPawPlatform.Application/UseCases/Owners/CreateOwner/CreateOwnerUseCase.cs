using VetPawPlatform.Application.Dto.Owners;
using VetPawPlatform.Domain.Exceptions;
using VetPawPlatform.Domain.Interfaces;
using VetPawPlatform.Domain.Entities;

namespace VetPawPlatform.Application.UseCases.Owners.CreateOwner;

public class CreateOwnerUseCase(IOwnerRepository repository)
{
    public async Task<OwnerResponseDto> ExecuteAsync(CreateOwnerDto dto)
    {
        var existingOwner = await repository.GetByDocumentAsync(dto.Document);

        if (existingOwner != null)
            throw new DomainException("Já existe um tutor cadastrado com este CPF.");

        var owner = new Owner(
            dto.Document,
            dto.FullName,
            dto.Email,
            dto.PhoneNumber,
            dto.BirthDate
        );

        await repository.CreateAsync(owner);

        return owner;
    }
}