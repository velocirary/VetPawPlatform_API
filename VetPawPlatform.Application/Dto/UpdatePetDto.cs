namespace VetPawPlatform.Application.Dto;
using VetPawPlatform.Domain.Enums;

public record UpdatePetDto(
    string Name,
    PetSpecies Species,
    DateTime BirthDate
);