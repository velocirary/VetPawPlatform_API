using FluentAssertions;
using Moq;
using VetPawPlatform.Application.UseCases.Appointments.CreateAppointment;
using VetPawPlatform.Domain.Entities;
using VetPawPlatform.Domain.Exceptions;
using VetPawPlatform.Tests.Builders;
using VetPawPlatform.Tests.Fixtures;

namespace VetPawPlatform.Tests.Application.Appointments;

public class CreateAppointmentUseCaseTests
{
    private readonly AppointmentUseCaseFixture _fixture;
    private readonly CreateAppointmentUseCase _useCase;

    public CreateAppointmentUseCaseTests()
    {
        _fixture = new AppointmentUseCaseFixture();
        _useCase = new CreateAppointmentUseCase(_fixture.RepositoryMock.Object);
    }

    [Fact]
    public async Task ShouldCreateAppointment_WhenDataIsValid()
    {        
        var dto = CreateAppointmentDtoBuilder.New().Build();

        var result = await _useCase.ExecuteAsync(dto);

        result.Should().NotBeNull();

        _fixture.RepositoryMock.Verify(
            repository => repository.CreateAsync(It.IsAny<Appointment>()),
            Times.Once
        );
    }

    [Fact]
    public async Task ShouldThrowException_WhenDateIsInThePast()
    {
        var dto = CreateAppointmentDtoBuilder
            .New()
            .WithDate(DateTime.UtcNow.AddDays(-1))
            .Build();

      
        Func<Task> action = async () => await _useCase.ExecuteAsync(dto);
        
        await action.Should()
            .ThrowAsync<DomainException>()
            .WithMessage("A data do agendamento não pode ser no passado.");
    }

    [Fact]
    public async Task ShouldThrowException_WhenOwnerIdIsInvalid()
    {        
        var dto = CreateAppointmentDtoBuilder
            .New()
            .WithOwnerId(Guid.Empty)
            .Build();

        Func<Task> action = async () => await _useCase.ExecuteAsync(dto);

        await action.Should()
            .ThrowAsync<DomainException>()
            .WithMessage("O identificador do tutor é obrigatório.");
    }

    [Fact]
    public async Task ShouldThrowException_WhenPetIdIsInvalid()
    {        
        var dto = CreateAppointmentDtoBuilder
            .New()
            .WithPetId(Guid.Empty)
            .Build();
             
        Func<Task> action = async () => await _useCase.ExecuteAsync(dto);
        
        await action.Should()
            .ThrowAsync<DomainException>()
            .WithMessage("O identificador do pet é obrigatório.");
    }
}