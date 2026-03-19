using VetPawPlatform.Domain.Enums;

namespace VetPawPlatform.Application.Dto.Appointments;

public record class UpdateAppointmentDto(    
    Guid PetId,
    DateTime Date,
    string? Reason,
    AppointmentStatus Status
);