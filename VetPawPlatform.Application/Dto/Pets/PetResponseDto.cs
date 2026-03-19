using VetPawPlatform.Domain.Enums;

namespace VetPawPlatform.Application.Dto.Pets;

public record PetResponseDto(
    Guid? Id,
    Guid? OwnerId,
    string Name,
    PetSpecies Species,
    DateTime BirthDate
)
{
    public static PetResponseDto? FromDomain(Pet? pet)
    {
        return new PetResponseDto(
            pet.Id,
            pet.OwnerId,
            pet.Name,
            pet.Species,
            pet.BirthDate
        );
    }
}