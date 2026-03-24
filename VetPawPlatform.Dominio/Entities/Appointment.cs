using VetPawPlatform.Domain.Enums;
using VetPawPlatform.Domain.Exceptions;

namespace VetPawPlatform.Domain.Entities;

public class Appointment
{
    public Guid Id { get; private set; }
    public Guid OwnerId { get; private set; }
    public Guid PetId { get; private set; }
    public DateTime Date { get; private set; }
    public string? Reason { get; private set; }
    public AppointmentStatus Status { get; private set; }

    public Appointment(Guid petId, Guid ownerId, DateTime date, string reason)
    {
        ValidateCreation(petId, ownerId, date, reason);

        Id = Guid.NewGuid();
        OwnerId = ownerId;
        PetId = petId;
        Date = date;
        Reason = reason;
        Status = AppointmentStatus.Scheduled;
    }

    public void Reschedule(DateTime newDate)
    {
        EnsureNotFinished();

        if (newDate < DateTime.UtcNow)
            throw new DomainException("A data do agendamento não pode ser no passado.");

        if (newDate > DateTime.UtcNow.AddMonths(6))
            throw new DomainException("A data do agendamento está muito distante.");

        Date = newDate;
    }
    
    public void ChangeReason(string reason)
    {
        EnsureNotFinished();

        if (string.IsNullOrWhiteSpace(reason))
            throw new DomainException("O motivo é obrigatório.");

        if (reason.Length > 200)
            throw new DomainException("O motivo deve ter no máximo 200 caracteres.");

        Reason = reason;
    }
    
    public void Complete()
    {
        if (Status == AppointmentStatus.Completed)
            throw new DomainException("O agendamento já está concluído.");

        if (Status == AppointmentStatus.Cancelled)
            throw new DomainException("Não é possível concluir um agendamento cancelado.");

        Status = AppointmentStatus.Completed;
    }

    public void Cancel()
    {
        if (Status == AppointmentStatus.Completed)
            throw new DomainException("Não é possível cancelar um agendamento concluído.");

        if (Status == AppointmentStatus.Cancelled)
            throw new DomainException("O agendamento já está cancelado.");

        Status = AppointmentStatus.Cancelled;
    }

    private void EnsureNotFinished()
    {
        if (Status == AppointmentStatus.Completed)
            throw new DomainException("Não é possível alterar um agendamento concluído.");

        if (Status == AppointmentStatus.Cancelled)
            throw new DomainException("Não é possível alterar um agendamento cancelado.");
    }

    public static Appointment Rehydrate(
        Guid id,
        Guid ownerId,
        Guid petId,
        DateTime date,
        string reason,
        AppointmentStatus status)
    {
        return new Appointment
        {
            Id = id,
            OwnerId = ownerId,
            PetId = petId,
            Date = date,
            Reason = reason,
            Status = status
        };
    }

    private static void ValidateCreation(Guid petId, Guid ownerId, DateTime date, string reason)
    {
        if (ownerId == Guid.Empty)
            throw new DomainException("O identificador do tutor é obrigatório.");

        if (petId == Guid.Empty)
            throw new DomainException("O identificador do pet é obrigatório.");

        if (date < DateTime.UtcNow)
            throw new DomainException("A data do agendamento não pode ser no passado.");

        if (date > DateTime.UtcNow.AddMonths(6))
            throw new DomainException("A data do agendamento está muito distante.");

        if (string.IsNullOrWhiteSpace(reason))
            throw new DomainException("O motivo é obrigatório.");

        if (reason.Length > 200)
            throw new DomainException("O motivo deve ter no máximo 200 caracteres.");
    }

    private Appointment() { }
}