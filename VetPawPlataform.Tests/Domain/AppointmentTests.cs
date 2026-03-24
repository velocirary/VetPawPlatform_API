using FluentAssertions;
using FluentAssertions.Execution;
using global::VetPawPlatform.Domain.Enums;
using global::VetPawPlatform.Domain.Exceptions;
using global::VetPawPlatform.Tests.Builders;

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
    public void UpdateDetails_ShouldUpdateAppointmentCorrectly()
    {
        var appointment = AppointmentBuilder.New().Build();

        var newDate = DateTime.UtcNow.AddDays(2);
        var newReason = "Consulta de rotina";
        var newStatus = AppointmentStatus.Completed;

        appointment.UpdateDetails(
            Guid.NewGuid(),
            newDate,
            newReason,
            newStatus
        );

        using (new AssertionScope())
        {
            appointment.Date.Should().Be(newDate);
            appointment.Reason.Should().Be(newReason);
            appointment.Status.Should().Be(newStatus);
        }
    }

    [Fact]
    public void UpdateDetails_ShouldThrowException_WhenDateIsInThePast()
    {
        var appointment = AppointmentBuilder.New().Build();

        var pastDate = DateTime.UtcNow.AddDays(-1);

        Action action = () => appointment.UpdateDetails(
            Guid.NewGuid(),
            pastDate,
            "Motivo",
            AppointmentStatus.Scheduled
        );

        action.Should()
            .Throw<DomainException>()
            .WithMessage("A data do agendamento não pode ser no passado.");
    }

    [Fact]
    public void UpdateDetails_ShouldThrowException_WhenStatusTransitionIsInvalid()
    {
        var appointment = AppointmentBuilder.New()
            .WithStatus(AppointmentStatus.Completed)
            .Build();

        Action action = () => appointment.UpdateDetails(
            Guid.NewGuid(),
            DateTime.UtcNow.AddDays(1),
            "Novo motivo",
            AppointmentStatus.Scheduled
        );

        action.Should()
            .Throw<DomainException>()
            .WithMessage("Não é possível alterar um agendamento concluído.");
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