using Amazon.DynamoDBv2.DataModel;
using VetPawPlatform.Domain.Entities;
using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Infra.DynamoDB.Repositories;

public class PetRepository(IDynamoDBContext context) : IPetRepository
{
    private readonly IDynamoDBContext _context = context;

    public async Task CreateAsync(Pet pet)
    {
        if (pet.Id == Guid.Empty)
            pet.Id = Guid.NewGuid();

        await _context.SaveAsync(pet);
    }

    public async Task<IEnumerable<Pet>> GetAllAsync()
    {
        return await _context.ScanAsync<Pet>([]).GetRemainingAsync();
    }

    public async Task<Pet?> GetByIdAsync(Guid id)
    {
        return await _context.LoadAsync<Pet>(id);
    }
}