using FluentAssertions;
using FluentAssertions.Execution;
using VetPawPlatform.Domain.Enums;
using VetPawPlatform.Domain.Exceptions;
using VetPawPlatform.Domain.ValueObjects;
using VetPawPlatform.Tests.Builders;

namespace VetPawPlatform.Tests.Domain;

public class OwnerTests
{
    [Fact]
    public void AddOwner_ShouldThrowException_WhenOwnerIsUnder18()
    {
        var birthDate = DateTime.UtcNow.AddYears(-17);

        Action action = () => OwnerBuilder.New()
            .WithBirthDate(birthDate)
            .Build();

        action.Should()
            .Throw<DomainException>()
            .WithMessage("O tutor deve ser maior de 18 anos.");
    }

    [Fact]
    public void AddPet_ShouldAddPetToCollection_WhenPetIsValid()
    {
        var owner = OwnerBuilder.New().Build();

        var pet = new Pet(
            owner.Id,
            new Name("Pikachu"),
            PetSpecies.Dog,
            DateTime.UtcNow.AddYears(-2)
        );

        owner.AddPet(pet);

        owner.Pets.Should().HaveCount(1);
        owner.Pets.Should().Contain(pet);
    }

    [Fact]
    public void Rehydrate_ShouldRestoreStateCorrectly()
    {
        var id = Guid.NewGuid();

        var owner = OwnerBuilder.New()
            .WithId(id)
            .BuildRehydrated();

        owner.Id.Should().Be(id);
        owner.Pets.Should().BeEmpty();
    }

    [Fact]
    public void AddPet_ShouldNotAddDuplicatePet()
    {
        var owner = OwnerBuilder.New().Build();
        var petId = Guid.NewGuid();

        var pet = Pet.Rehydrate(
            petId,
            owner.Id,
            new Name("Pikachu"),
            PetSpecies.Dog,
            DateTime.UtcNow.AddYears(-2)
        );

        owner.AddPet(pet);
        owner.AddPet(pet);

        owner.Pets.Should().HaveCount(1);
    }

    [Fact]
    public void AddPet_ShouldThrowException_WhenPetIsNull()
    {
        var owner = OwnerBuilder.New().Build();

        Action action = () => owner.AddPet(null!);

        action.Should()
            .Throw<DomainException>()
            .WithMessage("O pet não pode ser nulo.");
    }

    [Fact]
    public void UpdateDetails_ShouldUpdateOwnerData()
    {
        var owner = OwnerBuilder.New().Build();

        var updatedOwner = OwnerBuilder.New()
            .WithCpf("48765432100")
            .WithName("Brock")
            .WithEmail("brock@email.com")
            .WithPhone("159888888")
            .WithBirthDate(DateTime.UtcNow.AddYears(-25))
            .Build();

        owner.UpdateDetails(
            updatedOwner.Document,
            updatedOwner.FullName,
            updatedOwner.Email,
            updatedOwner.PhoneNumber,
            updatedOwner.BirthDate
        );

        using (new AssertionScope())
        {
            owner.Document.Should().Be(updatedOwner.Document);
            owner.FullName.Should().Be(updatedOwner.FullName);
            owner.Email.Should().Be(updatedOwner.Email);
            owner.PhoneNumber.Should().Be(updatedOwner.PhoneNumber);
            owner.BirthDate.Should().Be(updatedOwner.BirthDate);
        }
    }

    [Fact]
    public void UpdatePet_ShouldThrowException_WhenPetNotFound()
    {
        var owner = OwnerBuilder.New().Build();

        Action act = () => owner.UpdatePet(
            Guid.NewGuid(),
            new Name("Pikachu"),
            PetSpecies.Dog,
            DateTime.UtcNow.AddYears(-2)
        );

        act.Should()
            .Throw<DomainException>()
            .WithMessage("Pet não encontrado neste tutor.");
    }

    [Fact]
    public void AddPet_ShouldThrowException_WhenPetBelongsToAnotherOwner()
    {
        var owner1 = OwnerBuilder.New().Build();
        var owner2 = OwnerBuilder.New().Build();

        var pet = new Pet(
            owner1.Id,
            new Name("Pikachu"),
            PetSpecies.Dog,
            DateTime.UtcNow.AddYears(-2)
        );

        Action action = () => owner2.AddPet(pet);

        action.Should()
            .Throw<DomainException>()
            .WithMessage("Este pet não pertence a este tutor.");
    }
}