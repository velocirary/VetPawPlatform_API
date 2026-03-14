using VetPawPlatform.Domain.Exceptions;

namespace VetPawPlatform.Domain.ValueObjects;

public class Phone : ValueObject
{
    public string Number { get; }

    public Phone(string number)
    {
        if (string.IsNullOrWhiteSpace(number) || number.Length != 9)
            throw new DomainException("Telephone inválido. Deve conter 9 dígitos.");

        Number = number;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Number;
    }

    public static implicit operator Phone(string phone) => new(phone);
    public static implicit operator string(Phone phone) => phone?.Number ?? string.Empty;
}