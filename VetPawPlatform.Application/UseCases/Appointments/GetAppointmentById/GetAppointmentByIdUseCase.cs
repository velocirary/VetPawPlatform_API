using VetPawPlatform.Application.Dto.Appointments;
using VetPawPlatform.Domain.Exceptions;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Application.UseCases.Appointments.GetAppointmentById;

public class GetAppointmentByIdUseCase(IAppointmentRepository repository)
{
    public async Task<AppointmentResponseDto?> ExecuteAsync(Guid id)
    {
        var appointment = await repository.GetByIdAsync(id);

        return appointment == null ? throw new NotFoundException($"Agendamento com o id: '{id}' não encontrado") : (AppointmentResponseDto)appointment;
    }
}