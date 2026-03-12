using VetPawPlatform.Domain.Entities;

namespace VetPawPlatform.Domain.Interfaces;

public interface IOwnerRepository
{
    Task CreateAsync(Owner owner);
    Task UpdateAsync(Owner owner);
    Task<Owner?> GetByIdAsync(Guid id);
    Task<Owner?> GetByDocumentAsync(string document);
    Task<IEnumerable<Owner>> GetAllAsync();    
    Task<IEnumerable<Pet>> GetAllPetsAsync();
    Task<Pet?> GetPetByIdAsync(Guid petId);
}