using VetPawPlatform.Domain.Entities;
using VetPawPlatform.Domain.Enums;

namespace VetPawPlatform.Application.Dto.Appointments;

public record AppointmentResponseDto(
    Guid? Id,
    Guid OwnerId,
    Guid PetId,
    DateTime Date,
    string? Reason,
    AppointmentStatus Status
)
{
    public static implicit operator AppointmentResponseDto(Appointment appointment)
    {
        return new AppointmentResponseDto(
            appointment.Id,
            appointment.OwnerId,
            appointment.PetId,
            appointment.Date,
            appointment.Reason,
            appointment.Status
        );
    }
}

