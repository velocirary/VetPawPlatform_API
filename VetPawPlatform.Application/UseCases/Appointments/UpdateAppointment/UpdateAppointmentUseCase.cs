using VetPawPlatform.Application.Dto.Appointments;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Application.UseCases.Appointments.UpdateAppointment;

public class UpdateAppointmentUseCase(IAppointmentRepository repository)
{
    public async Task<AppointmentResponseDto?> ExecuteAsync(Guid id, UpdateAppointmentDto dto)
    {
        var appointment = await repository.GetByIdAsync(id);

        if (appointment == null)
            return null;

        appointment.UpdateDetails(
            dto.PetId,
            dto.Date,
            dto.Reason,
            dto.Status            
        );

        await repository.UpdateAsync(appointment);

        return appointment;
    }
}
