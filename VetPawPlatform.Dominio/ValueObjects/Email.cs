using VetPawPlatform.Domain.Exceptions;

namespace VetPawPlatform.Domain.ValueObjects;

public class Email : ValueObject
{
    public string Address { get; }

    public Email(string address)
    {
        if (string.IsNullOrWhiteSpace(address) || !address.Contains('@'))
            throw new DomainException("E-mail inválido.");

        Address = address;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Address;
    }

    public static implicit operator Email(string email) => new(email);
    public static implicit operator string(Email email) => email?.Address ?? string.Empty;
}