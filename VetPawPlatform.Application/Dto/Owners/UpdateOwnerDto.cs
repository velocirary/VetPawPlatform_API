namespace VetPawPlatform.Application.Dto.Owners;

public record UpdateOwnerDto(
    string FullName,
    string Email,
    string Document,
    string PhoneNumber,
    DateTime BirthDate
);