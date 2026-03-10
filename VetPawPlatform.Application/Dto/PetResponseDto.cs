using VetPawPlatform.Domain.Entities;
using VetPawPlatform.Domain.Enums;

public record PetResponseDto(
    Guid? Id,
    string Name,
    PetSpecies Species,
    DateTime BirthDate
)
{
    public static implicit operator PetResponseDto(Pet pet)
    {
        return new PetResponseDto(
            pet.Id,
            pet.Name,
            pet.Species,
            pet.BirthDate
        );
    }
}