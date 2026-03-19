using VetPawPlatform.Application.Dto.Appointments;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Application.UseCases.Appointment.GetAllAppointment;

public class GetAllAppointmentUseCase(IAppointmentRepository repository)
{
    public async Task<IEnumerable<AppointmentResponseDto>> ExecuteAsync()
    {
        var appointments = await repository.GetAllAppointmentsAsync();

        return appointments.Select(appointment => (AppointmentResponseDto)appointment);
    }
}