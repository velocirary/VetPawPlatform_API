using VetPawPlatform.Domain.Entities;

namespace VetPawPlatform.Application.Dto.Owners;

public record OwnerResponseDto(
    Guid? Id,
    string FullName,
    string Email,
    string Document,
    string PhoneNumber,
    DateTime BirthDate
)
{
    public static implicit operator OwnerResponseDto(Owner Owner)
    {
        return new OwnerResponseDto(
            Owner.Id,
            Owner.FullName,
            Owner.Email,
            Owner.Document,
            Owner.PhoneNumber,
            Owner.BirthDate
        );
    }
}