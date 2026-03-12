using VetPawPlatform.Domain.Entities;

namespace VetPawPlatform.Domain.Interfaces;

public interface IOwnerRepository
{
    Task CreateAsync(Owner pet);
    Task<Owner?> GetByDocumentAsync(string document);
    Task<Owner?> GetByIdAsync(Guid id);
    Task<IEnumerable<Owner>> GetAllAsync();
}