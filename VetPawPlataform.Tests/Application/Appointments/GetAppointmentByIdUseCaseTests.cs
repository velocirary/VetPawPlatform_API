using FluentAssertions;
using Moq;
using VetPawPlatform.Application.UseCases.Appointments.GetAppointmentById;
using VetPawPlatform.Domain.Entities;
using VetPawPlatform.Domain.Exceptions;
using VetPawPlatform.Tests.Builders;
using VetPawPlatform.Tests.Fixtures;

namespace VetPawPlatform.Tests.Application.Appointments;

public class GetAppointmentByIdUseCaseTests
{
    private readonly AppointmentUseCaseFixture _fixture;
    private readonly GetAppointmentByIdUseCase _useCase;

    public GetAppointmentByIdUseCaseTests()
    {
        _fixture = new AppointmentUseCaseFixture();
        _useCase = new GetAppointmentByIdUseCase(_fixture.RepositoryMock.Object);
    }

    [Fact]
    public async Task ShouldReturnAppointment_WhenAppointmentExists()
    {
        var appointment = AppointmentBuilder.New().Build();

        _fixture.RepositoryMock
            .Setup(repo => repo.GetByIdAsync(appointment.Id))
            .ReturnsAsync(appointment);

        var result = await _useCase.ExecuteAsync(appointment.Id);

        result.Should().NotBeNull();
        result!.Id.Should().Be(appointment.Id);

        _fixture.RepositoryMock.Verify(
            repo => repo.GetByIdAsync(appointment.Id),
            Times.Once
        );
    }

    [Fact]
    public async Task ShouldThrowNotFoundException_WhenAppointmentDoesNotExist()
    {
        var id = Guid.NewGuid();

        _fixture.RepositoryMock
            .Setup(repo => repo.GetByIdAsync(id))
            .ReturnsAsync((Appointment?)null);

        Func<Task> act = async () => await _useCase.ExecuteAsync(id);

        await act.Should()
            .ThrowAsync<NotFoundException>()
            .WithMessage($"Agendamento com o id: '{id}' não encontrado");
    }
}