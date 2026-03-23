using VetPawPlatform.Domain.Entities;
using VetPawPlatform.Domain.Enums;

namespace VetPawPlatform.Tests.Builders;

public class AppointmentBuilder
{
    private Guid _id = Guid.NewGuid();
    private Guid _ownerId = Guid.NewGuid();
    private Guid _petId = Guid.NewGuid();
    private DateTime _date = DateTime.UtcNow.AddDays(1);
    private string _reason = "Consulta";
    private AppointmentStatus _status = AppointmentStatus.Scheduled;

    public static AppointmentBuilder New() => new();

    public Appointment Build()
    {
        return Appointment.Rehydrate(
            _id,
            _ownerId,
            _petId,
            _date,
            _reason,
            _status
        );
    }
}