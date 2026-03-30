using VetPawPlatform.Application.Common.Security;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Application.UseCases.Users;

public class LoginUseCase(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtService jwtService)
{
    private readonly IUserRepository _repository = userRepository;
    private readonly IPasswordHasher _hasher = passwordHasher;
    private readonly IJwtService _jwt = jwtService;

    public async Task<string> Execute(string email, string password)
    {
        var user = await _repository.GetByEmailAsync(email);

        if (user is null || !_hasher.Verify(password, user.PasswordHash))
            throw new Exception("Credenciais inválidas");

        return _jwt.GenerateToken(user.Id, user.Email, user.Role.ToString());
    }
}