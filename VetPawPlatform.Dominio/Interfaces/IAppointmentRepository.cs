using VetPawPlatform.Domain.Entities;

namespace VetPawPlatform.Domain.Interfaces;

public interface IAppointmentRepository
{
    Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
}
