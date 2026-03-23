using FluentAssertions;
using Moq;
using VetPawPlatform.Application.UseCases.Appointments.GetAllAppointment;
using VetPawPlatform.Domain.Entities;
using VetPawPlatform.Tests.Builders;
using VetPawPlatform.Tests.Fixtures;

namespace VetPawPlatform.Tests.Application.Appointments;

public class GetAllAppointmentUseCaseTests
{
    private readonly AppointmentUseCaseFixture _fixture;
    private readonly GetAllAppointmentUseCase _useCase;

    public GetAllAppointmentUseCaseTests()
    {
        _fixture = new AppointmentUseCaseFixture();
        _useCase = new GetAllAppointmentUseCase(_fixture.RepositoryMock.Object);
    }

    [Fact]
    public async Task ShouldReturnAllAppointments()
    {        
        var appointments = new List<Appointment>
        {
            AppointmentBuilder.New().Build(),
            AppointmentBuilder.New().Build()
        };

        _fixture.RepositoryMock
            .Setup(repo => repo.GetAllAppointmentsAsync())
            .ReturnsAsync(appointments);

        var result = await _useCase.ExecuteAsync();

        result.Should().NotBeNull();
        result.Should().HaveCount(2);

        _fixture.RepositoryMock.Verify(
            repo => repo.GetAllAppointmentsAsync(),
            Times.Once
        );
    }

    [Fact]
    public async Task ShouldReturnEmptyList_WhenNoAppointmentsExist()
    {
        _fixture.RepositoryMock
            .Setup(repo => repo.GetAllAppointmentsAsync())
            .ReturnsAsync([]);

        var result = await _useCase.ExecuteAsync();

        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task ShouldMapAppointmentsToResponseDto()
    {
        var appointment = AppointmentBuilder.New().Build();

        _fixture.RepositoryMock
            .Setup(repo => repo.GetAllAppointmentsAsync())
            .ReturnsAsync([appointment]);

        var result = await _useCase.ExecuteAsync();

        var dto = result.First();

        dto.Should().NotBeNull();
        dto.Id.Should().Be(appointment.Id);
    }
}