using VetPawPlatform.Domain.Enums;

namespace VetPawPlatform.Application.Dto.Pets;

public record UpdatePetDto(
    string Name,
    PetSpecies Species,
    DateTime BirthDate
);