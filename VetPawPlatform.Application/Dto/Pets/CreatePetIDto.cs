using VetPawPlatform.Domain.Enums;

namespace VetPawPlatform.Application.Dto.Pets;

public record CreatePetDto(
    Guid OwnerId,
    string Name,
    PetSpecies Species,
    DateTime BirthDate
);