using VetPawPlatform.Application.Dto.Appointments;
using VetPawPlatform.Domain.Enums;

namespace VetPawPlatform.Tests.Builders;

public class UpdateAppointmentDtoBuilder
{
    private Guid _petId = Guid.NewGuid();
    private DateTime _date = DateTime.UtcNow.AddDays(1);
    private string _reason = "Atualização";
    private AppointmentStatus _status = AppointmentStatus.Scheduled;

    public static UpdateAppointmentDtoBuilder New()
        => new();

    public UpdateAppointmentDtoBuilder WithDate(DateTime date)
    {
        _date = date;
        return this;
    }

    public UpdateAppointmentDtoBuilder WithPetId(Guid petId)
    {
        _petId = petId;
        return this;
    }

    public UpdateAppointmentDtoBuilder WithReason(string reason)
    {
        _reason = reason;
        return this;
    }

    public UpdateAppointmentDtoBuilder WithStatus(AppointmentStatus status)
    {
        _status = status;
        return this;
    }

    public UpdateAppointmentDto Build()
        => new(_petId, _date, _reason, _status);
}