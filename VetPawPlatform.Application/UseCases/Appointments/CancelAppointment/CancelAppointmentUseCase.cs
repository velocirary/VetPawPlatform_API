using VetPawPlatform.Application.Dto.Appointments;
using VetPawPlatform.Domain.Exceptions;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Application.UseCases.Appointments.CancelAppointment;

public class CancelAppointmentUseCase(IAppointmentRepository repository)
{
    public async Task<AppointmentResponseDto> ExecuteAsync(Guid id)
    {
        var appointment = await repository.GetByIdAsync(id) 
            ?? throw new NotFoundException($"Agendamento com id '{id}' não encontrado.");

        appointment.Cancel();

        await repository.UpdateAsync(appointment);

        return appointment;
    }
}