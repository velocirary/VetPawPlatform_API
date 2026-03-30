using Microsoft.AspNetCore.Mvc;
using VetPawPlatform.Application.Common.Security;
using VetPawPlatform.Application.Dto.Users;
using VetPawPlatform.Domain.Entities;
using VetPawPlatform.Domain.Enums;
using VetPawPlatform.Domain.Interfaces;
namespace VetPawPlatform.API.Controller;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;

    public AuthController(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto loginRequest)
    {
        var user = await _userRepository.GetByEmailAsync(loginRequest.Email);

        if (user is null)
            return Unauthorized("Invalid credentials");

        var passwordValid = _passwordHasher.Verify(loginRequest.Password, user.PasswordHash);

        if (!passwordValid)
            return Unauthorized("Invalid credentials");

        var token = _jwtService.GenerateToken(user.Id, user.Email, user.Role.ToString());

        return Ok(new
        {
            token
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestDto registerRequest)
    {
        var existingUser = await _userRepository.GetByEmailAsync(registerRequest.Email);

        if (existingUser is not null)
            return Conflict("User already exists");

        var passwordHash = _passwordHasher.Hash(registerRequest.Password);

        var user = new User(
            email: registerRequest.Email,
            passwordHash: passwordHash,
            role: UserRole.Tutor
        );

        await _userRepository.CreateAsync(user);

        return Ok();
    }
}