using FluentAssertions;
using VetPawPlatform.Domain.Enums;
using VetPawPlatform.Domain.Exceptions;
using VetPawPlatform.Tests.Builders;

namespace VetPawPlatform.Tests.Domain;

public class AppointmentTests
{
    [Fact]
    public void CreateAppointment_ShouldThrowException_WhenDateIsInThePast()
    {
        var pastDate = DateTime.UtcNow.AddDays(-1);

        Action action = () => AppointmentBuilder.New()
            .WithDate(pastDate)
            .Build();

        action.Should()
            .Throw<DomainException>()
            .WithMessage("A data do agendamento não pode ser no passado.");
    }

    [Fact]
    public void CreateAppointment_ShouldCreateSuccessfully_WhenDataIsValid()
    {
        var appointment = AppointmentBuilder.New().Build();

        appointment.Should().NotBeNull();
        appointment.Status.Should().Be(AppointmentStatus.Scheduled);
    }

    [Fact]
    public void Reschedule_ShouldUpdateDate_WhenValid()
    {
        var appointment = AppointmentBuilder.New().Build();
        var newDate = DateTime.UtcNow.AddDays(3);

        appointment.Reschedule(newDate);

        appointment.Date.Should().Be(newDate);
    }

    [Fact]
    public void Reschedule_ShouldThrowException_WhenDateIsInThePast()
    {
        var appointment = AppointmentBuilder.New().Build();
        var pastDate = DateTime.UtcNow.AddDays(-1);

        Action action = () => appointment.Reschedule(pastDate);

        action.Should()
            .Throw<DomainException>()
            .WithMessage("A data do agendamento não pode ser no passado.");
    }

    [Fact]
    public void ChangeReason_ShouldUpdateReason_WhenValid()
    {
        var appointment = AppointmentBuilder.New().Build();
        var newReason = "Consulta de rotina";

        appointment.ChangeReason(newReason);

        appointment.Reason.Should().Be(newReason);
    }

    [Fact]
    public void ChangeReason_ShouldThrowException_WhenReasonIsTooLong()
    {
        var appointment = AppointmentBuilder.New().Build();
        var longReason = new string('a', 201);

        Action action = () => appointment.ChangeReason(longReason);

        action.Should()
            .Throw<DomainException>()
            .WithMessage("O motivo deve ter no máximo 200 caracteres.");
    }

    [Fact]
    public void Complete_ShouldChangeStatus_WhenValid()
    {
        var appointment = AppointmentBuilder.New().Build();

        appointment.Complete();

        appointment.Status.Should().Be(AppointmentStatus.Completed);
    }

    [Fact]
    public void Complete_ShouldThrowException_WhenAlreadyCompleted()
    {
        var appointment = AppointmentBuilder.New()
            .WithStatus(AppointmentStatus.Completed)
            .BuildRehydrated();

        Action action = () => appointment.Complete();

        action.Should()
            .Throw<DomainException>()
            .WithMessage("O agendamento já está concluído.");
    }

    [Fact]
    public void Cancel_ShouldChangeStatus_WhenValid()
    {
        var appointment = AppointmentBuilder.New().Build();

        appointment.Cancel();

        appointment.Status.Should().Be(AppointmentStatus.Cancelled);
    }

    [Fact]
    public void Cancel_ShouldThrowException_WhenAlreadyCompleted()
    {
        var appointment = AppointmentBuilder.New()
            .WithStatus(AppointmentStatus.Completed)
            .BuildRehydrated();

        Action action = () => appointment.Cancel();

        action.Should()
            .Throw<DomainException>()
            .WithMessage("Não é possível cancelar um agendamento concluído.");
    }

    [Fact]
    public void Rehydrate_ShouldRestoreStateCorrectly()
    {
        var id = Guid.NewGuid();

        var appointment = AppointmentBuilder.New()
            .WithId(id)
            .BuildRehydrated();

        appointment.Id.Should().Be(id);
    }
}