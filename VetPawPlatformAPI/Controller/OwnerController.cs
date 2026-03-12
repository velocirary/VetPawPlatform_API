using Microsoft.AspNetCore.Mvc;
using VetPawPlatform.Application.Dto.Owners;
using VetPawPlatform.Application.UseCases.Pets.GetPetById;

namespace VetPawPlatform.API.Controller;

[ApiController]
[Route("api/owner")]
public class OwnerController(
    CreateOwnerUseCase createOwner,
    GetPetByIdUseCase getOwnerById) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOwnerDto createPetDto)
    {
        var responseCreateOwner = await createOwner.ExecuteAsync(createPetDto);
        return CreatedAtAction(nameof(GetById), new { id = responseCreateOwner.Id }, responseCreateOwner);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var responseOwnerById = await getOwnerById.ExecuteAsync(id);
        return Ok(responseOwnerById);
    }
}