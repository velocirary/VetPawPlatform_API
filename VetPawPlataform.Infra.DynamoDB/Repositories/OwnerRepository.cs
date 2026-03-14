using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using VetPawPlatform.Domain.Entities;
using VetPawPlatform.Domain.Enums;
using VetPawPlatform.Domain.Interfaces;
using VetPawPlatform.Infra.Models;

namespace VetPawPlatform.Infra.DynamoDB.Repositories;

public class OwnerRepository(IDynamoDBContext context) : IOwnerRepository
{
    private readonly IDynamoDBContext _context = context;

    public async Task CreateAsync(Owner owner)
    {
        var dbModel = MapToDbModel(owner);
        await _context.SaveAsync(dbModel);
    }

    public async Task UpdateAsync(Owner owner)
    {
        var dbModel = MapToDbModel(owner);
        await _context.SaveAsync(dbModel);
    }

    public async Task<IEnumerable<Owner>> GetAllAsync()
    {
        var dbModels = await _context.ScanAsync<OwnerDbModel>([]).GetRemainingAsync();
        return dbModels.Select(MapToDomain);
    }

    public async Task<Owner?> GetByIdAsync(Guid id)
    {
        var ownerDb = await _context.LoadAsync<OwnerDbModel>(id);
        return ownerDb == null ? null : MapToDomain(ownerDb);
    }

    public async Task<Owner?> GetByDocumentAsync(string document)
    {        
        var queryConfig = new QueryOperationConfig
        {
            IndexName = "Document-Index",
            Filter = new QueryFilter("Document", QueryOperator.Equal, document)
        };

        var search = _context.FromQueryAsync<OwnerDbModel>(queryConfig);

        var results = await search.GetRemainingAsync();
        var ownerDb = results.FirstOrDefault();

        return ownerDb == null ? null : MapToDomain(ownerDb);
    }

    public async Task<IEnumerable<Pet>> GetAllPetsAsync()
    {
        var owners = await _context.ScanAsync<OwnerDbModel>([]).GetRemainingAsync();
        return owners.SelectMany(owner => owner.Pets.Select(pet => MapPetToDomain(pet, owner.Id)));
    }

    public async Task<Pet?> GetPetByIdAsync(Guid petId)
    {
        var owners = await _context.ScanAsync<OwnerDbModel>([]).GetRemainingAsync();

        foreach (var owner in owners)
        {
            var petDb = owner.Pets.FirstOrDefault(pet => pet.Id == petId);
            if (petDb != null) return MapPetToDomain(petDb, owner.Id);
        }

        return null;
    }

    private static Owner MapToDomain(OwnerDbModel dbModel)
    {
        var pets = dbModel.Pets?.Select(pet => MapPetToDomain(pet, dbModel.Id)) ?? [];

        return Owner.Rehydrate(
            dbModel.Id,
            dbModel.Document,
            dbModel.FullName,
            dbModel.Email,
            dbModel.PhoneNumber,
            DateTime.Parse(dbModel.BirthDate),
            pets
        );
    }

    private static Pet MapPetToDomain(PetDbModel petDbModel, Guid ownerId)
    {
        return Pet.Rehydrate(
            petDbModel.Id,
            ownerId,
            petDbModel.Name,
            (PetSpecies)petDbModel.Species,
            DateTime.Parse(petDbModel.BirthDate)
        );
    }

    private static OwnerDbModel MapToDbModel(Owner owner)
    {
        return new OwnerDbModel
        {
            Id = owner.Id,
            FullName = owner.FullName,
            Document = owner.Document.Number,
            Email = owner.Email.Address,
            PhoneNumber = owner.PhoneNumber,
            BirthDate = owner.BirthDate.ToString("O"),
            Pets = [.. owner.Pets.Select(pet => new PetDbModel
            {
                Id = pet.Id,
                Name = pet.Name,
                Species = (int)pet.Species,
                BirthDate = pet.BirthDate.ToString("O"),
                IdOwner = owner.Id
            })]
        };
    }
}