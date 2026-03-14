using VetPawPlatform.Domain.Exceptions;

namespace VetPawPlatform.Domain.ValueObjects;

public class Name : ValueObject
{
    public string Value { get; }

    public Name(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Contains("string"))
            throw new DomainException("O preenchimento de nome é obrigatório");

        this.Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator Name(string name) => new(name);
    public static implicit operator string(Name name) => name?.Value ?? string.Empty;
}