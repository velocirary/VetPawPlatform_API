using VetPawPlatform.Domain.Entities;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Infra.Security;

public class UserRepository : IUserRepository
{
    public Task<User?> GetByEmail(string email)
    {     
        return Task.FromResult<User?>(null);
    }

    public Task Create(User user)
    {     
        return Task.CompletedTask;
    }
}