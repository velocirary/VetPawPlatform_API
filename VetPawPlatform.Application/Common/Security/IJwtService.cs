namespace VetPawPlatform.Application.Common.Security;

public interface IJwtService
{
    string GenerateToken(Guid userId, string email, string role);
}