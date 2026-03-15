using FluentAssertions;
using Moq;
using VetPawPlatform.Application.UseCases.Owners.GetOwnerById;
using VetPawPlatform.Domain.Entities;
using VetPawPlatform.Domain.Exceptions;
using VetPawPlatform.Tests.Builders;
using VetPawPlatform.Tests.Fixtures;

namespace VetPawPlatform.Tests.Application.Owners;

public class GetOwnerByIdUseCaseTests
{
    private readonly OwnerUseCaseFixture _fixture;
    private readonly GetOwnerByIdUseCase _useCaseById;

    public GetOwnerByIdUseCaseTests()
    {
        _fixture = new OwnerUseCaseFixture();
        _useCaseById = new GetOwnerByIdUseCase(_fixture.RepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnOwner_WhenOwnerExists()
    {
        var owner = new OwnerBuilder().Build();

        _fixture.RepositoryMock
            .Setup(ownerRepository => ownerRepository.GetByIdAsync(owner.Id))
            .ReturnsAsync(owner);

        var result = await _useCaseById.ExecuteAsync(owner.Id);

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowNotFound_WhenOwnerDoesNotExist()
    {
        var id = Guid.NewGuid();

        _fixture.RepositoryMock
            .Setup(ownerRepository => ownerRepository.GetByIdAsync(id))
            .ReturnsAsync((Owner?)null);

        Func<Task> action = async () => await _useCaseById.ExecuteAsync(id);

        await action.Should().ThrowAsync<NotFoundException>();
    }
}