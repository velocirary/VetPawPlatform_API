using FluentAssertions;
using Moq;
using VetPawPlatform.Application.UseCases.Appointments.CompleteAppointment;
using VetPawPlatform.Domain.Enums;
using VetPawPlatform.Domain.Exceptions;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Tests.Application.Appointments;

public class CompleteAppointmentUseCaseTests
{
    [Fact]
    public async Task Should_Complete_Appointment_When_Valid()
    {
        var repositoryMock = new Mock<IAppointmentRepository>();

        var appointment = Appointment.Rehydrate(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateTime.UtcNow.AddDays(1),
            "Consulta",
            AppointmentStatus.Scheduled
        );

        repositoryMock
            .Setup(repository => repository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(appointment);

        var useCase = new CompleteAppointmentUseCase(repositoryMock.Object);
        
        var result = await useCase.ExecuteAsync(appointment.Id);
        
        result.Should().NotBeNull();
        result.Status.Should().Be(AppointmentStatus.Completed);

        repositoryMock.Verify(repository => repository.UpdateAsync(appointment), Times.Once);
    }

    [Fact]
    public async Task Should_Throw_NotFoundException_When_Appointment_Not_Found()
    {        
        var repositoryMock = new Mock<IAppointmentRepository>();

        repositoryMock
            .Setup(repository => repository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Appointment?)null);

        var useCase = new CompleteAppointmentUseCase(repositoryMock.Object);
        
        Func<Task> action = async () => await useCase.ExecuteAsync(Guid.NewGuid());
        
        await action.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task Should_Throw_DomainException_When_Already_Completed()
    {        
        var repositoryMock = new Mock<IAppointmentRepository>();

        var appointment = Appointment.Rehydrate(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateTime.UtcNow.AddDays(1),
            "Consulta",
            AppointmentStatus.Completed
        );

        repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(appointment);

        var useCase = new CompleteAppointmentUseCase(repositoryMock.Object);

        Func<Task> action = async () => await useCase.ExecuteAsync(appointment.Id);

        await action.Should().ThrowAsync<DomainException>();

        repositoryMock.Verify(repository => repository.UpdateAsync(It.IsAny<Appointment>()), Times.Never);
    }
}
