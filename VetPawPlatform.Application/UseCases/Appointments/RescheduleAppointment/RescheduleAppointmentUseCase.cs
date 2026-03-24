using VetPawPlatform.Application.Dto.Appointments;
using VetPawPlatform.Application.Dto.Pets;
using VetPawPlatform.Domain.Exceptions;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Application.UseCases.Appointments.RescheduleAppointment;

public class RescheduleAppointmentUseCase(IAppointmentRepository repository)
{
    public async Task<AppointmentResponseDto> ExecuteAsync(Guid id, RescheduleAppointmentDto dto)
    {
        var appointment = await repository.GetByIdAsync(id) 
            ?? throw new NotFoundException($"Agendamento com id '{id}' não encontrado.");

        appointment.Reschedule(dto.NewDate);

        await repository.UpdateAsync(appointment);

        return appointment;
    }
}