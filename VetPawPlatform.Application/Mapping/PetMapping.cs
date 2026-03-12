using VetPawPlatform.Domain.Entities;
using VetPawPlatform.Application.Dto.Pets;

namespace VetPawPlatform.Application.Mappings;

public static class PetMapping
{
    public static PetResponseDto ToResponse(this Pet pet)
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