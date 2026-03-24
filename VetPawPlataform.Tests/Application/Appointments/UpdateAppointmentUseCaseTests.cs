using FluentAssertions;
using Moq;
using VetPawPlatform.Application.UseCases.Appointments.UpdateAppointment;
using VetPawPlatform.Domain.Entities;
using VetPawPlatform.Domain.Exceptions;
using VetPawPlatform.Tests.Builders;
using VetPawPlatform.Tests.Fixtures;

namespace VetPawPlatform.Tests.Application.Appointments;

public class UpdateAppointmentUseCaseTests
{
    private readonly AppointmentUseCaseFixture _fixture;
    private readonly UpdateAppointmentUseCase _useCase;

    public UpdateAppointmentUseCaseTests()
    {
        _fixture = new AppointmentUseCaseFixture();
        _useCase = new UpdateAppointmentUseCase(_fixture.RepositoryMock.Object);
    }

    [Fact]
    public async Task ShouldUpdateAppointment_WhenDataIsValid()
    {        
        var appointment = AppointmentBuilder.New().Build();
        var dto = UpdateAppointmentDtoBuilder.New().Build();

        _fixture.RepositoryMock
            .Setup(repo => repo.GetByIdAsync(appointment.Id))
            .ReturnsAsync(appointment);
     
        var result = await _useCase.ExecuteAsync(appointment.Id, dto);
        
        result.Should().NotBeNull();

        _fixture.RepositoryMock.Verify(
            repo => repo.UpdateAsync(It.IsAny<Appointment>()),
            Times.Once
        );
    }

    [Fact]
    public async Task ShouldReturnNull_WhenAppointmentDoesNotExist()
    {
        var id = Guid.NewGuid();
        var dto = UpdateAppointmentDtoBuilder.New().Build();

        _fixture.RepositoryMock
            .Setup(repo => repo.GetByIdAsync(id))
            .ReturnsAsync((Appointment?)null);

        var result = await _useCase.ExecuteAsync(id, dto);

        result.Should().BeNull();

        _fixture.RepositoryMock.Verify(
            repo => repo.UpdateAsync(It.IsAny<Appointment>()),
            Times.Never
        );
    }

    [Fact]
    public async Task ShouldThrowException_WhenDateIsInvalid()
    {
        var appointment = AppointmentBuilder.New().Build();

        var dto = UpdateAppointmentDtoBuilder
            .New()
            .WithDate(DateTime.UtcNow.AddDays(-1))
            .Build();

        _fixture.RepositoryMock
            .Setup(repo => repo.GetByIdAsync(appointment.Id))
            .ReturnsAsync(appointment);

        Func<Task> action = async () => await _useCase.ExecuteAsync(appointment.Id, dto);

        await action.Should()
            .ThrowAsync<DomainException>();

        _fixture.RepositoryMock.Verify(
            repo => repo.UpdateAsync(It.IsAny<Appointment>()),
            Times.Never
        );
    }
}