using Microsoft.AspNetCore.Mvc;
using VetPawPlatform.Application.Dto.Appointments;
using VetPawPlatform.Application.Dto.Pets;
using VetPawPlatform.Application.UseCases.Appointments.CancelAppointment;
using VetPawPlatform.Application.UseCases.Appointments.CompleteAppointment;
using VetPawPlatform.Application.UseCases.Appointments.CreateAppointment;
using VetPawPlatform.Application.UseCases.Appointments.GetAllAppointment;
using VetPawPlatform.Application.UseCases.Appointments.GetAppointmentById;
using VetPawPlatform.Application.UseCases.Appointments.RescheduleAppointment;
using VetPawPlatform.Application.UseCases.Appointments.UpdateAppointment;

namespace VetPawPlatform.API.Controller;

[ApiController]
[Route("api/appointments")]
public class AppointmentController(
    CreateAppointmentUseCase createAppointment,
    GetAppointmentByIdUseCase getAppointmentById,
    GetAllAppointmentUseCase getAllAppointment,
    UpdateAppointmentUseCase updateAppointment,
    CompleteAppointmentUseCase completeAppointment,
    CancelAppointmentUseCase cancelAppointment,
    RescheduleAppointmentUseCase rescheduleAppointment
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentDto dto)
    {
        var response = await createAppointment.ExecuteAsync(dto);

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await getAppointmentById.ExecuteAsync(id);

        if (response == null)
            return NotFound(new { message = "Agendamento não encontrado." });

        return Ok(response);
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
        var response = await updateAppointment.ExecuteAsync(id, dto);
        return Ok(response);
    }

    [HttpPatch("{id:guid}/complete")]
    public async Task<IActionResult> Complete(Guid id)
    {
        var response = await completeAppointment.ExecuteAsync(id);
        return Ok(response);
    }

    [HttpPatch("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var response = await cancelAppointment.ExecuteAsync(id);
        return Ok(response);
    }

    [HttpPatch("{id:guid}/reschedule")]
    public async Task<IActionResult> Reschedule(Guid id, [FromBody] RescheduleAppointmentDto dto)
    {
        var response = await rescheduleAppointment.ExecuteAsync(id, dto);
        return Ok(response);
    }
}