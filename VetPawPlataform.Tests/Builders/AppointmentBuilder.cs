using VetPawPlatform.Domain.Entities;
using VetPawPlatform.Domain.Enums;

namespace VetPawPlatform.Tests.Builders;

public class AppointmentBuilder
{
    private Guid _id = Guid.NewGuid();
    private readonly Guid _ownerId = Guid.NewGuid();
    private readonly Guid _petId = Guid.NewGuid();
    private DateTime _date = DateTime.UtcNow.AddDays(1);
    private readonly string _reason = "Consulta";
    private AppointmentStatus _status = AppointmentStatus.Scheduled;

    public static AppointmentBuilder New() => new();

    public Appointment Build()
    {
        return new Appointment(
            _petId,
            _ownerId,
            _date,
            _reason,
            _status
        );
    }
    public AppointmentBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public AppointmentBuilder WithDate(DateTime date)
    {
        _date = date;
        return this;
    }

    public AppointmentBuilder WithStatus(AppointmentStatus status)
    {
        _status = status;
        return this;
    }

    public Appointment BuildRehydrated()
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