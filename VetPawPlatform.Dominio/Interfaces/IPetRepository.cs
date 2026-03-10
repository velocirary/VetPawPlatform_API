using VetPawPlatform.Domain.Entities;

namespace VetPawPlatform.Domain.Interfaces;

public interface IPetRepository
{
    Task CreateAsync(Pet pet);
    Task<Pet?> GetByIdAsync(Guid id);
    Task<IEnumerable<Pet>> GetAllAsync();
    Task UpdateAsync(Pet pet);
}