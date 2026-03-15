using VetPawPlatform.Application.Dto.Owners;

namespace VetPawPlatform.Tests.Builders;

public static class UpdateOwnerDtoBuilder
{
    public static UpdateOwnerDto Create()
    {
        return new UpdateOwnerDto(
            "Gary Carvalho",
            "gary@email.com",
            "42425624424",
            "159999999",
            DateTime.UtcNow.AddYears(-20)
        );
    }
}

