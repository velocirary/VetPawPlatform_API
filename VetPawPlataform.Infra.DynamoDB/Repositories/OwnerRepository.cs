using Amazon.DynamoDBv2.DataModel;
using VetPawPlatform.Domain.Entities;
using VetPawPlatform.Domain.Interfaces;
using VetPawPlatform.Infra.Models;

namespace VetPawPlatform.Infra.Repositories;

public class OwnerRepository(IDynamoDBContext context) : IOwnerRepository
{
    private readonly IDynamoDBContext _context = context;

    public async Task CreateAsync(Owner owner)
    {
        var dbModel = MapToDbModel(owner);
        await _context.SaveAsync(dbModel);
    }

    public async Task<IEnumerable<Owner>> GetAllAsync()
    {
        var dbModels = await _context.ScanAsync<OwnerDbModel>([]).GetRemainingAsync();

        return dbModels.Select(ownerDb => Owner.Rehydrate(
            ownerDb.Id,
            ownerDb.Document,
            ownerDb.FullName,
            ownerDb.Email,
            ownerDb.PhoneNumber,
            DateTime.Parse(ownerDb.BirthDate))
        );
    }

    public async Task<Owner?> GetByIdAsync(Guid id)
    {
        var ownerDb = await _context.LoadAsync<OwnerDbModel>(id);

        if (ownerDb == null)
            return null;

        return Owner.Rehydrate(
            ownerDb.Id,
            ownerDb.Document,
            ownerDb.FullName,
            ownerDb.Email,
            ownerDb.PhoneNumber,
            DateTime.Parse(ownerDb.BirthDate)
        );
    }

    public async Task UpdateAsync(Owner owner)
    {
        var dbModel = MapToDbModel(owner);
        await _context.SaveAsync(dbModel);
    }

    private static OwnerDbModel MapToDbModel(Owner owner)
    {
        return new OwnerDbModel
        {
            Id = owner.Id,
            FullName = owner.FullName,
            Document = owner.Document,
            Email = owner.Email,
            PhoneNumber = owner.PhoneNumber,
            BirthDate = owner.BirthDate.ToString("O")
        };
    }

    public async Task<Owner?> GetByDocumentAsync(string document)
    {
        var conditions = new List<ScanCondition>
        {
            new("Document", Amazon.DynamoDBv2.DocumentModel.ScanOperator.Equal, document)
        };

        var search = _context.ScanAsync<OwnerDbModel>(conditions);
        var results = await search.GetRemainingAsync();
        var ownerDb = results.FirstOrDefault();

        if (ownerDb == null) return null;

        return Owner.Rehydrate(
            ownerDb.Id,
            ownerDb.Document,
            ownerDb.FullName,
            ownerDb.Email,
            ownerDb.PhoneNumber,
            DateTime.Parse(ownerDb.BirthDate)
        );
    }
}