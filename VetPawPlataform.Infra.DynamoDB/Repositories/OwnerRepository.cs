using Amazon.DynamoDBv2.DataModel;
using VetPawPlatform.Domain.Entities;
using VetPawPlatform.Domain.Enums;
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
        var conditions = new List<ScanCondition>
        {
            new("Document", Amazon.DynamoDBv2.DocumentModel.ScanOperator.Equal, document)
        };

        var results = await _context.ScanAsync<OwnerDbModel>(conditions).GetRemainingAsync();
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

    private static Pet MapPetToDomain(PetDbModel p, Guid ownerId)
    {
        return Pet.Rehydrate(
            p.Id,
            ownerId,
            p.Name,
            (PetSpecies)p.Species,
            DateTime.Parse(p.BirthDate)
        );
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