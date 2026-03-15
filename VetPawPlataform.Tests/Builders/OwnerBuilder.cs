using VetPawPlatform.Domain.Entities;
using VetPawPlatform.Domain.ValueObjects;

namespace VetPawPlatform.Tests.Builders;

public class OwnerBuilder
{
    private string _cpf = "40028922400";
    private string _name = "Ash Ketchum";
    private string _email = "ash@email.com";
    private string _phone = "159999999";
    private DateTime _birthDate = DateTime.UtcNow.AddYears(-25);
    private Guid _id = Guid.NewGuid();

    public static OwnerBuilder New()
    {
        return new OwnerBuilder();
    }

    public OwnerBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public OwnerBuilder WithCpf(string cpf)
    {
        _cpf = cpf;
        return this;
    }

    public OwnerBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public OwnerBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public OwnerBuilder WithPhone(string phone)
    {
        _phone = phone;
        return this;
    }

    public OwnerBuilder WithBirthDate(DateTime birthDate)
    {
        _birthDate = birthDate;
        return this;
    }

    public Owner Build()
    {
        return new Owner(
            new Cpf(_cpf),
            new Name(_name),
            new Email(_email),
            new Phone(_phone),
            _birthDate
        );
    }

    public Owner BuildRehydrated()
    {
        return Owner.Rehydrate(
            _id,
            new Cpf(_cpf),
            new Name(_name),
            new Email(_email),
            new Phone(_phone),
            _birthDate
        );
    }
}