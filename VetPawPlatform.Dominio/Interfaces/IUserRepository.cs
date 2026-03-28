using VetPawPlatform.Domain.Entities;

namespace VetPawPlatform.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmail(string email);
    Task Create(User user);
}