using VetPawPlatform.Domain.Entities;

namespace VetPawPlatform.Domain.Interfaces;

public interface IAppointmentRepository
{
    Task CreateAsync(Appointment appointment);
    Task UpdateAsync(Appointment appointment);
    Task<Appointment?> GetByIdAsync(Guid id);
    Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
}