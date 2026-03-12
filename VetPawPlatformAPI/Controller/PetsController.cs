using Microsoft.AspNetCore.Mvc;
using VetPawPlatform.Application.Dto.Pets;
using VetPawPlatform.Application.UseCases.Pets.CreatePet;
using VetPawPlatform.Application.UseCases.Pets.GetAllPet;
using VetPawPlatform.Application.UseCases.Pets.GetPetById;
using VetPawPlatform.Application.UseCases.Pets.UpdatePet;

namespace VetPawPlatform.API.Controller;

[ApiController]
[Route("api/pets")]
public class PetsController(
    CreatePetUseCase createPet,
    GetPetByIdUseCase getPetById,
    GetAllPetsUseCase getAllPets,
    UpdatePetUseCase updatePet) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePetDto createPetDto)
    {
        var responseCreatePet = await createPet.ExecuteAsync(createPetDto);
        return CreatedAtAction(nameof(GetById), new { id = responseCreatePet.Id }, responseCreatePet);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var responsePetById = await getPetById.ExecuteAsync(id);
        return Ok(responsePetById);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var responseAllPets = await getAllPets.ExecuteAsync();
        return Ok(responseAllPets);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePetDto updatePetDto)
    {
        var response = await updatePet.ExecuteAsync(id, updatePetDto);
        return Ok(response);
    }
}