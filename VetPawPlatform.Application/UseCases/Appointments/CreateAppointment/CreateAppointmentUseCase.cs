using VetPawPlatform.Application.Dto.Appointments;
using VetPawPlatform.Domain.Entities;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Application.UseCases.Appointments.CreateAppointment;

public class CreateAppointmentUseCase(IAppointmentRepository repository)
{
    public async Task<AppointmentResponseDto> ExecuteAsync(CreateAppointmentDto dto)
    {
        var appointment = new Appointment(
            dto.PetId,
            dto.OwnerId,
            dto.Date,
            dto.Reason ?? string.Empty
        );

        await repository.CreateAsync(appointment);

        return appointment;
    }
}
