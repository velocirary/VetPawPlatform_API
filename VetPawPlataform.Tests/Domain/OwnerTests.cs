using FluentAssertions;
using VetPawPlatform.Domain.Entities;
using VetPawPlatform.Domain.Exceptions;
using VetPawPlatform.Domain.ValueObjects;

namespace VetPawPlatform.Tests.Domain;

public class OwnerTests
{
    [Fact]
    public void AddOwner_OwnerIsUnder18()
    {        
        var birthDate = DateTime.UtcNow.AddYears(-17);
        var cpf = new Cpf("12345678901");
        var name = new Name("Teste");
        var email = new Email("teste@email.com");
        var phone = new Phone("112354789");
       
        Action act = () => new Owner(cpf, name, email, phone, birthDate);
        
        act.Should().Throw<DomainException>()
           .WithMessage("O tutor deve ser maior de 18 anos.");
    }

    [Fact]
    public void AddPet_ShouldAddPetToCollection_WhenPetIsValid()
    {
        var owner = CreateValidOwner();

        var pet = new Pet(owner.Id, new Name("Rex"), VetPawPlatform.Domain.Enums.PetSpecies.Dog, DateTime.UtcNow.AddYears(-2));

        owner.AddPet(pet);

        owner.Pets.Should().HaveCount(1);
        owner.Pets.Should().Contain(pet);
    }

    private Owner CreateValidOwner()
        => new(new Cpf("12345678901"), new Name("John"), new Email("j@j.com"), new Phone("112354789"), DateTime.UtcNow.AddYears(-20));
}