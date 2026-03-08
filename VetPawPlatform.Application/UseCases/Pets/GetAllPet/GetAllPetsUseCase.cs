using VetPawPlatform.Domain.Interfaces;

namespace VetPawPlatform.Application.UseCases.Pets.GetAllPet;

public class GetAllPetsUseCase(IPetRepository repository)
{
    public async Task<IEnumerable<PetResponseDto>> ExecuteAsync()
    {
        var pets = await repository.GetAllAsync();

        return pets.Select(pet => (PetResponseDto)pet);
    }
}