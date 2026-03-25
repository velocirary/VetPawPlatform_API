using FluentAssertions;
using Moq;
using VetPawPlatform.Application.Dto.Pets;
using VetPawPlatform.Application.UseCases.Appointments.RescheduleAppointment;
using VetPawPlatform.Domain.Entities;
using VetPawPlatform.Domain.Enums;
using VetPawPlatform.Domain.Exceptions;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Tests.Application.Appointments;

public class RescheduleAppointmentUseCaseTests
{
    [Fact]
    public async Task Should_Reschedule_Appointment_When_Valid()
    {        
        var repositoryMock = new Mock<IAppointmentRepository>();

        var appointment = Appointment.Rehydrate(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateTime.UtcNow.AddDays(2),
            "Consulta",
            AppointmentStatus.Scheduled
        );

        var dto = new RescheduleAppointmentDto(DateTime.UtcNow.AddDays(5));

        repositoryMock
            .Setup(repository => repository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(appointment);

        var useCase = new RescheduleAppointmentUseCase(repositoryMock.Object);
        
        var result = await useCase.ExecuteAsync(appointment.Id, dto);
        
        result.Date.Should().Be(dto.NewDate);

        repositoryMock.Verify(repository => repository.UpdateAsync(appointment), Times.Once);
    }

    [Fact]
    public async Task Should_Throw_NotFoundException_When_Appointment_Not_Found()
    {        
        var repositoryMock = new Mock<IAppointmentRepository>();

        repositoryMock
            .Setup(repository => repository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Appointment?)null);

        var useCase = new RescheduleAppointmentUseCase(repositoryMock.Object);

        var dto = new RescheduleAppointmentDto(DateTime.UtcNow.AddDays(5));
        
        Func<Task> action = async () => await useCase.ExecuteAsync(Guid.NewGuid(), dto);
        
        await action.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task Should_Throw_DomainException_When_Date_Is_In_The_Past()
    {
        var repositoryMock = new Mock<IAppointmentRepository>();

        var appointment = Appointment.Rehydrate(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateTime.UtcNow.AddDays(2),
            "Consulta",
            AppointmentStatus.Scheduled
        );

        var dto = new RescheduleAppointmentDto(DateTime.UtcNow.AddDays(-1));

        repositoryMock
            .Setup(repository => repository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(appointment);

        var useCase = new RescheduleAppointmentUseCase(repositoryMock.Object);

        Func<Task> action = async () => await useCase.ExecuteAsync(appointment.Id, dto);

        await action.Should().ThrowAsync<DomainException>();

        repositoryMock.Verify(repository => repository.UpdateAsync(It.IsAny<Appointment>()), Times.Never);
    }
}
