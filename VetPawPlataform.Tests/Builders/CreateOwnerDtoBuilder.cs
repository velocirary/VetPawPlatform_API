using VetPawPlatform.Application.Dto.Owners;

namespace VetPawPlatform.Tests.Builders;

public class CreateOwnerDtoBuilder
{
    private string _document = "40028922400";
    private string _fullName = "Ash Ketchum";
    private readonly string _email = "ash@email.com";
    private readonly string _phoneNumber = "119999999";
    private readonly DateTime _birthDate = DateTime.UtcNow.AddYears(-20);

    public static CreateOwnerDtoBuilder New()
    {
        return new CreateOwnerDtoBuilder();
    }

    public CreateOwnerDtoBuilder WithDocument(string document)
    {
        _document = document;
        return this;
    }

    public CreateOwnerDtoBuilder WithName(string name)
    {
        _fullName = name;
        return this;
    }

    public CreateOwnerDto Build()
    {
        return new CreateOwnerDto(
            _document,
            _fullName,
            _email,
            _phoneNumber,
            _birthDate
        );
    }
}