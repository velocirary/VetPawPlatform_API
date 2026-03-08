namespace VetPawPlatform.Application.Dto;

public record CreatePetDto(
    string Name,
    string Species,
    DateTime BirthDate
);