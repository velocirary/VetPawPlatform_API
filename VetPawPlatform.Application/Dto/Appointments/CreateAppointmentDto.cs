using VetPawPlatform.Domain.Enums;

namespace VetPawPlatform.Application.Dto.Appointments;

public record class CreateAppointmentDto(
    Guid OwnerId,
    Guid PetId,
    DateTime Date,
    string? Reason,
    AppointmentStatus Status
);
