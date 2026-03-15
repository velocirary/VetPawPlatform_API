using FluentAssertions;
using global::VetPawPlatform.Application.UseCases.Owners.GetAllOwner;
using global::VetPawPlatform.Domain.Entities;
using Moq;
using VetPawPlatform.Tests.Builders;
using VetPawPlatform.Tests.Fixtures;

namespace VetPawPlatform.Tests.Application.Owners;

public class GetAllOwnerUseCaseTests
{
    private readonly OwnerUseCaseFixture _fixture;
    private readonly GetAllOwnerUseCase _useCaseGetAllOwner;

    public GetAllOwnerUseCaseTests()
    {
        _fixture = new OwnerUseCaseFixture();
        _useCaseGetAllOwner = new GetAllOwnerUseCase(_fixture.RepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnListOfOwners_WhenOwnersExist()
    {
        var owners = new List<Owner>
        {
            OwnerBuilder.New().Build(),
            OwnerBuilder.New().Build()

        };

        _fixture.RepositoryMock
            .Setup(ownerRepository => ownerRepository.GetAllAsync())
            .ReturnsAsync(owners);

        var result = await _useCaseGetAllOwner.ExecuteAsync();

        result.Should().HaveCount(2);
        _fixture.RepositoryMock.Verify(ownerRepository => ownerRepository.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnEmptyList_WhenNoOwnersExist()
    {
        _fixture.RepositoryMock
            .Setup(ownerRepository => ownerRepository.GetAllAsync())
            .ReturnsAsync([]);
        
        var result = await _useCaseGetAllOwner.ExecuteAsync();

        result.Should().BeEmpty();
    }
}