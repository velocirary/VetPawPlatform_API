using Microsoft.AspNetCore.Mvc;
using VetPawPlatform.Application.Dto.Pets;
using VetPawPlatform.Application.UseCases.Pets.GetAllPet;
using VetPawPlatform.Application.UseCases.Pets.GetPetById;
using VetPawPlatform.Application.UseCases.Pets.UpdatePet;

namespace VetPawPlatform.API.Controller;

[ApiController]
[Route("api/pets")]
public class PetsController(
    GetPetByIdUseCase getPetById,
    GetAllPetsUseCase getAllPets,
    UpdatePetUseCase updatePet) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var responseAllPets = await getAllPets.ExecuteAsync();
        return Ok(responseAllPets);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var responsePetById = await getPetById.ExecuteAsync(id);
        return Ok(responsePetById);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePetDto updatePetDto)
    {
        var responseUpdatePet = await updatePet.ExecuteAsync(id, updatePetDto);
        return Ok(responseUpdatePet);
    }
}