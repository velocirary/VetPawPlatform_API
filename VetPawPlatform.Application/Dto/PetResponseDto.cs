using VetPawPlatform.Domain.Entities;

public record PetResponseDto(
    Guid? Id,
    string Name,
    string Species,
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