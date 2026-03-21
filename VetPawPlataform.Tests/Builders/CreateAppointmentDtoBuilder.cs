using VetPawPlatform.Application.Dto.Appointments;
using VetPawPlatform.Domain.Enums;

namespace VetPawPlatform.Tests.Builders;

public class CreateAppointmentDtoBuilder
{
    private Guid _ownerId = Guid.NewGuid();
    private Guid _petId = Guid.NewGuid();
    private DateTime _date = DateTime.UtcNow.AddDays(1);
    private string _reason = "Consulta de rotina";
    private AppointmentStatus _status = AppointmentStatus.Scheduled;

    public static CreateAppointmentDtoBuilder New()
    {
        return new CreateAppointmentDtoBuilder();
    }

    public CreateAppointmentDtoBuilder WithOwnerId(Guid ownerId)
    {
        _ownerId = ownerId;
        return this;
    }

    public CreateAppointmentDtoBuilder WithPetId(Guid petId)
    {
        _petId = petId;
        return this;
    }

    public CreateAppointmentDtoBuilder WithDate(DateTime date)
    {
        _date = date;
        return this;
    }

    public CreateAppointmentDtoBuilder WithReason(string reason)
    {
        _reason = reason;
        return this;
    }

    public CreateAppointmentDtoBuilder WithStatus(AppointmentStatus status)
    {
        _status = status;
        return this;
    }

    public CreateAppointmentDto Build()
    {
        return new CreateAppointmentDto(
            _ownerId,
            _petId,
            _date,
            _reason,
            _status
        );
    }
}