using VetPawPlatform.Application.Dto.Appointments;
using VetPawPlatform.Domain.Enums;
using VetPawPlatform.Domain.Exceptions;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Application.UseCases.Appointments.UpdateAppointment;

public class UpdateAppointmentUseCase(IAppointmentRepository repository)
{
    public async Task<AppointmentResponseDto> ExecuteAsync(Guid id, UpdateAppointmentDto dto)
    {
        var appointment = await repository.GetByIdAsync(id);

        if (appointment == null)
            throw new NotFoundException($"Agendamento com o id: '{id}' não encontrado");

        if (dto.Date != appointment.Date)
            appointment.Reschedule(dto.Date);

        if (dto.Reason != appointment.Reason)
        {
            if (dto.Reason is null)
                throw new DomainException("O motivo é obrigatório.");

            appointment.ChangeReason(dto.Reason);
        }

        if (dto.Status != appointment.Status)
        {
            switch (dto.Status)
            {
                case AppointmentStatus.Completed:
                    appointment.Complete();
                    break;

                case AppointmentStatus.Cancelled:
                    appointment.Cancel();
                    break;

                case AppointmentStatus.Scheduled:
                    throw new DomainException("Transição de status inválida.");
            }
        }

        await repository.UpdateAsync(appointment);

        return appointment;
    }
}