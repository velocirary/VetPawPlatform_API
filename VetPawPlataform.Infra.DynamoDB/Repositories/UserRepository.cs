using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using VetPawPlatform.Domain.Entities;
using VetPawPlatform.Domain.Enums;
using VetPawPlatform.Domain.Interfaces;
using VetPawPlatform.Infra.DynamoDB.Models;

namespace VetPawPlatform.Infra.DynamoDB.Repositories;

public class UserRepository(IDynamoDBContext context) : IUserRepository
{
    private readonly IDynamoDBContext _context = context;

    public async Task CreateAsync(User user)
    {
        var dbModel = MapToDbModel(user);
        await _context.SaveAsync(dbModel);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var queryConfig = new QueryOperationConfig
        {
            IndexName = "Email-index",
            Limit = 1,
            KeyExpression = new Expression
            {
                ExpressionStatement = "Email = :v_email",
                ExpressionAttributeValues = new Dictionary<string, DynamoDBEntry>
                {
                    { ":v_email", email }
                }
            }
        };

        var search = _context.FromQueryAsync<UserDbModel>(queryConfig);

        var results = await search.GetNextSetAsync();

        var dbModel = results.FirstOrDefault();

        return dbModel is null ? null : MapToDomain(dbModel);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        var dbModel = await _context.LoadAsync<UserDbModel>(id);

        return dbModel is null ? null : MapToDomain(dbModel);
    }

    private static User MapToDomain(UserDbModel dbModel)
    {
        if (!Enum.IsDefined(typeof(UserRole), dbModel.Role))
            throw new Exception("Invalid user role");

        return new User(
            email: dbModel.Email,
            passwordHash: dbModel.PasswordHash,
            role: (UserRole)dbModel.Role
        );
    }

    private static UserDbModel MapToDbModel(User user)
    {
        return new UserDbModel
        {
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            Role = (int)user.Role
        };
    }
}