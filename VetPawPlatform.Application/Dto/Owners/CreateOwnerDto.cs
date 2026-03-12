namespace VetPawPlatform.Application.Dto.Owners;

public record CreateOwnerDto(
    string FullName,
    string Email,
    string Document,
    string PhoneNumber,
    DateTime BirthDate
);