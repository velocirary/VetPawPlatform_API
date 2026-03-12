using VetPawPlatform.Application.Dto.Owners;
using VetPawPlatform.Domain.Entities;

namespace VetPawPlatform.Application.Mappings;

public static class OwnerMapping
{
    public static OwnerResponseDto ToResponse(this Owner owner)
    {
        return new OwnerResponseDto(
            owner.Id,
            owner.FullName,
            owner.Email,
            owner.Document,
            owner.PhoneNumber,
            owner.BirthDate
        );
    }
}