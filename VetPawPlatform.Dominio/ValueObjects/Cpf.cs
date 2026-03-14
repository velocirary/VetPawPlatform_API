using VetPawPlatform.Domain.Exceptions;

namespace VetPawPlatform.Domain.ValueObjects;

public class Cpf : ValueObject
{
    public string Number { get; }

    public Cpf(string number)
    {
        if (string.IsNullOrWhiteSpace(number) || number.Length != 11)
            throw new DomainException("CPF inválido. Deve conter 11 dígitos.");

        Number = number;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Number;
    }

    public static implicit operator Cpf(string number) => new(number);
    public static implicit operator string(Cpf cpf) => cpf?.Number ?? string.Empty;    
}