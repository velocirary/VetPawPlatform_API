using FluentAssertions;
using Moq;
using VetPawPlatform.Application.Dto.Pets;
using VetPawPlatform.Application.UseCases.Owners.AddPetToOwner;
using VetPawPlatform.Domain.Enums;
using VetPawPlatform.Tests.Builders;
using VetPawPlatform.Tests.Fixtures;

namespace VetPawPlatform.Tests.Application.Owners;

public class AddPetToOwnerUseCaseTests
{
    private readonly OwnerUseCaseFixture _fixture;
    private readonly AddPetToOwnerUseCase _useCase;

    public AddPetToOwnerUseCaseTests()
    {
        _fixture = new OwnerUseCaseFixture();
        _useCase = new AddPetToOwnerUseCase(_fixture.RepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldAddPet_WhenOwnerExists()
    {
        var owner = new OwnerBuilder().Build();

        var dto = new CreatePetDto(
            owner.Id,
            "Pikachu",
            PetSpecies.Dog,
            DateTime.UtcNow.AddYears(-2)
        );

        _fixture.RepositoryMock
            .Setup(ownerRepository => ownerRepository.GetByIdAsync(owner.Id))
            .ReturnsAsync(owner);

        var result = await _useCase.ExecuteAsync(owner.Id, dto);

        owner.Pets.Should().HaveCount(1);

        _fixture.RepositoryMock.Verify(ownerRepository => ownerRepository.UpdateAsync(owner), Times.Once);
    }
}