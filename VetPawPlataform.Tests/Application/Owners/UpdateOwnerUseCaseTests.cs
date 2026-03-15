using FluentAssertions;
using Moq;
using VetPawPlatform.Application.UseCases.Owners.UpdateOwner;
using VetPawPlatform.Domain.Entities;
using VetPawPlatform.Tests.Builders;
using VetPawPlatform.Tests.Fixtures;

namespace VetPawPlatform.Tests.Application.Owners;

public class UpdateOwnerUseCaseTests
{
    private readonly OwnerUseCaseFixture _fixture;
    private readonly UpdateOwnerUseCase _useCase;

    public UpdateOwnerUseCaseTests()
    {
        _fixture = new OwnerUseCaseFixture();
        _useCase = new UpdateOwnerUseCase(_fixture.RepositoryMock.Object);
    }

    [Fact]
    public async Task ShouldReturnNull_WhenOwnerDoesNotExist()
    {        
        var id = Guid.NewGuid();

        var dto = UpdateOwnerDtoBuilder.Create();

        _fixture.RepositoryMock
            .Setup(ownerRepository => ownerRepository.GetByIdAsync(id))
            .ReturnsAsync((Owner?)null);

        var result = await _useCase.ExecuteAsync(id, dto);

        result.Should().BeNull();
    }

    [Fact]
    public async Task ShouldUpdateOwner_WhenOwnerExists()
    {
        var owner = OwnerBuilder.New().Build();

        var dto = UpdateOwnerDtoBuilder.Create();

        _fixture.RepositoryMock
            .Setup(ownerRepository => ownerRepository.GetByIdAsync(owner.Id))
            .ReturnsAsync(owner);

        var result = await _useCase.ExecuteAsync(owner.Id, dto);

        result.Should().NotBeNull();

        _fixture.RepositoryMock.Verify(
            ownerRepository => ownerRepository.UpdateAsync(owner),
            Times.Once
        );
    }
}