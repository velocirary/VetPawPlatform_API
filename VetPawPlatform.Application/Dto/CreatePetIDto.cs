using VetPawPlatform.Domain.Enums;

namespace VetPawPlatform.Application.Dto;

public record CreatePetDto(
    string Name,
    PetSpecies Species,
    DateTime BirthDate
);