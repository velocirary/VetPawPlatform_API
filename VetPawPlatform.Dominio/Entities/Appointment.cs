using VetPawPlatform.Domain.Enums;

namespace VetPawPlatform.Domain.Entities;

public class Appointment
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public Guid PetId { get; set; }
    public DateTime Date { get; set; }
    public string? Reason { get; set; }
    public AppointmentStatus Status { get; set; }
}