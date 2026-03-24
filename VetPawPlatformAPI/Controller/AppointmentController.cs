using Microsoft.AspNetCore.Mvc;
using VetPawPlatform.Application.Dto.Appointments;
using VetPawPlatform.Application.UseCases.Appointments.CreateAppointment;
using VetPawPlatform.Application.UseCases.Appointments.GetAllAppointment;
using VetPawPlatform.Application.UseCases.Appointments.GetAppointmentById;
using VetPawPlatform.Application.UseCases.Appointments.UpdateAppointment;

namespace VetPawPlatform.API.Controller;

[ApiController]
[Route("api/appointment")]
public class AppointmentController(
    CreateAppointmentUseCase createAppointment,
    GetAppointmentByIdUseCase getAppointmentById,
    GetAllAppointmentUseCase getAllAppointment,
    UpdateAppointmentUseCase updateAppointment
    ) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentDto createAppointmentDto)
    {
        var responseCreateAppointment = await createAppointment.ExecuteAsync(createAppointmentDto);
        return CreatedAtAction(nameof(GetById), new { id = responseCreateAppointment.Id }, responseCreateAppointment);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var responseAppointmentById = await getAppointmentById.ExecuteAsync(id);

        if (responseAppointmentById == null)
            return NotFound(new { message = "Agendamento não encontrado." });

        return Ok(responseAppointmentById);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await getAllAppointment.ExecuteAsync();
        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAppointmentDto dto)
    {
        var responseUpdate = await updateAppointment.ExecuteAsync(id, dto);

        if (responseUpdate == null)
            return NotFound(new { message = "Agendamento não encontrado para atualização." });

        return Ok(responseUpdate);
    }
}