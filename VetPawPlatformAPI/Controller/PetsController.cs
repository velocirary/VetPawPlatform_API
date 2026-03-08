using Microsoft.AspNetCore.Mvc;
using VetPawPlatform.Application.Dto;
using VetPawPlatform.Application.UseCases.Pets.GetAllPet;
using VetPawPlatform.Application.UseCases.Pets.GetPetById;

namespace VetPawPlatform.API.Controller;

[ApiController]
[Route("api/pets")]
public class PetsController(
    CreatePetUseCase createPet,
    GetPetByIdUseCase getPetById,
    GetAllPetsUseCase getAllPetsUseCase) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreatePetDto createPetDto)
    {
        var responseCreatePet = await createPet.ExecuteAsync(createPetDto);
        return CreatedAtAction(nameof(GetById), new { id = responseCreatePet.Id }, responseCreatePet);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var responsePetById = await getPetById.ExecuteAsync(id);

        if (responsePetById == null)
            return NotFound(new { message = $"Pet com ID {id} não encontrado." });

        return Ok(responsePetById);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var responseAllPets = await getAllPetsUseCase.ExecuteAsync();

        return Ok(responseAllPets);
    }
}