using FluentAssertions;
using Moq;
using VetPawPlatform.Application.UseCases.Owners.CreateOwner;
using VetPawPlatform.Domain.Entities;
using VetPawPlatform.Domain.Exceptions;
using VetPawPlatform.Tests.Builders;
using VetPawPlatform.Tests.Fixtures;

namespace VetPawPlatform.Tests.Application.Owners;

public class CreateOwnerUseCaseTests
{
    private readonly OwnerUseCaseFixture _fixture;
    private readonly CreateOwnerUseCase _useCase;

    public CreateOwnerUseCaseTests()
    {
        _fixture = new OwnerUseCaseFixture();
        _useCase = new CreateOwnerUseCase(_fixture.RepositoryMock.Object);
    }

    [Fact]
    public async Task ShouldCreateOwner_WhenDocumentDoesNotExist()
    {
        var dto = CreateOwnerDtoBuilder.New().Build();

        _fixture.RepositoryMock
            .Setup(ownerRepository => ownerRepository.GetByDocumentAsync(dto.Document))
            .ReturnsAsync((Owner?)null);

        var result = await _useCase.ExecuteAsync(dto);

        result.Should().NotBeNull();

        _fixture.RepositoryMock.Verify(
            ownerRepository => ownerRepository.CreateAsync(It.IsAny<Owner>()),
            Times.Once
        );
    }

    [Fact]
    public async Task ShouldThrowException_WhenDocumentAlreadyExists()
    {        
        var dto = CreateOwnerDtoBuilder.New().Build();

        var existingOwner = OwnerBuilder.New().Build();

        _fixture.RepositoryMock
            .Setup(owerRepository => owerRepository.GetByDocumentAsync(dto.Document))
            .ReturnsAsync(existingOwner);
        
        Func<Task> act = async () => await _useCase.ExecuteAsync(dto);

        await act.Should()
            .ThrowAsync<DomainException>()
            .WithMessage("Já existe um tutor cadastrado com este CPF.");
    }
}