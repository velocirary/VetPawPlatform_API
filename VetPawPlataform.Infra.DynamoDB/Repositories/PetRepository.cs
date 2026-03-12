using Amazon.DynamoDBv2.DataModel;
using VetPawPlatform.Domain.Entities;
using VetPawPlatform.Domain.Enums;
using VetPawPlatform.Domain.Interfaces;
using VetPawPlatform.Infra.Models;

namespace VetPawPlatform.Infra.Repositories;

public class PetRepository(IDynamoDBContext context) : IPetRepository
{
    private readonly IDynamoDBContext _context = context;

    public async Task CreateAsync(Pet pet)
    {        
        var dbModel = MapToDbModel(pet);
        await _context.SaveAsync(dbModel);
    }

    public async Task<IEnumerable<Pet>> GetAllAsync()
    {
        var dbModels = await _context.ScanAsync<PetDbModel>([]).GetRemainingAsync();
        
        return dbModels.Select(petDb => Pet.Rehydrate(
            petDb.Id,
            petDb.IdOwner,
            petDb.Name,
            (PetSpecies)petDb.Species,
            DateTime.Parse(petDb.BirthDate))
        );
    }

    public async Task<Pet?> GetByIdAsync(Guid id)
    {
        var dbModel = await _context.LoadAsync<PetDbModel>(id);

        if (dbModel == null)
            return null;

        return Pet.Rehydrate(
            dbModel.Id,
            dbModel.IdOwner,
            dbModel.Name,
            (PetSpecies)dbModel.Species,
            DateTime.Parse(dbModel.BirthDate)
        );
    }

    public async Task UpdateAsync(Pet pet)
    {
        var dbModel = MapToDbModel(pet);
        await _context.SaveAsync(dbModel);
    }
    
    private static PetDbModel MapToDbModel(Pet pet)
    {
        return new PetDbModel
        {
            Id = pet.Id,
            Name = pet.Name,
            Species = (int)pet.Species,
            BirthDate = pet.BirthDate.ToString("O")
        };
    }
}