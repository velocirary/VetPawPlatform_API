using VetPawPlatform.Application.Dto.Appointments;
using VetPawPlatform.Domain.Exceptions;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Application.UseCases.Appointments.CompleteAppointment;

public class CompleteAppointmentUseCase(IAppointmentRepository repository)
{
    public async Task<AppointmentResponseDto> ExecuteAsync(Guid id)
    {
        var appointment = await repository.GetByIdAsync(id);

        if (appointment == null)
            throw new NotFoundException($"Agendamento com id '{id}' não encontrado.");

        appointment.Complete();

        await repository.UpdateAsync(appointment);

        return appointment;
    }
}