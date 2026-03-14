using Microsoft.AspNetCore.Mvc;
using VetPawPlatform.Application.Dto.Owners;
using VetPawPlatform.Application.Dto.Pets;
using VetPawPlatform.Application.UseCases.Owners.AddPetToOwner;
using VetPawPlatform.Application.UseCases.Owners.CreateOwner;
using VetPawPlatform.Application.UseCases.Owners.GetAllOwner;
using VetPawPlatform.Application.UseCases.Owners.GetOwnerById;
using VetPawPlatform.Application.UseCases.Owners.UpdateOwner;

namespace VetPawPlatform.API.Controller;

[ApiController]
[Route("api/owners")]
public class OwnerController(
    CreateOwnerUseCase createOwner,
    GetOwnerByIdUseCase getOwnerById,
    GetAllOwnerUseCase getAllOwner,
    UpdateOwnerUseCase updateOwner,
    AddPetToOwnerUseCase addPetToOwner) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOwnerDto createOwnerDto)
    {
        var responseCreateOwner = await createOwner.ExecuteAsync(createOwnerDto);
        return CreatedAtAction(nameof(GetById), new { id = responseCreateOwner.Id }, responseCreateOwner);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var responseOwnerById = await getOwnerById.ExecuteAsync(id);

        if (responseOwnerById == null)
            return NotFound(new { message = "Tutor não encontrado." });

        return Ok(responseOwnerById);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await getAllOwner.ExecuteAsync();
        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOwnerDto dto)
    {
        var responseUpdate = await updateOwner.ExecuteAsync(id, dto);

        if (responseUpdate == null)
            return NotFound(new { message = "Tutor não encontrado para atualização." });

        return Ok(responseUpdate);
    }

    [HttpPost("{ownerId}/pets")]
    public async Task<IActionResult> AddPet(Guid ownerId, [FromBody] CreatePetDto dto)
    {
        var result = await addPetToOwner.ExecuteAsync(ownerId, dto);
        return Ok(result);
    }
}