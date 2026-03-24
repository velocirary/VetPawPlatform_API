using VetPawPlatform.Domain.Enums;
using VetPawPlatform.Domain.Exceptions;

namespace VetPawPlatform.Domain.Entities;

public class Appointment
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public Guid PetId { get; set; }
    public DateTime Date { get; set; }
    public string? Reason { get; set; }
    public AppointmentStatus Status { get; set; }

    public Appointment(Guid petId, Guid ownerId, DateTime date, string reason, AppointmentStatus status)
    {
        ValidateCreation(petId, ownerId, date, reason, status);

        Id = Guid.NewGuid();
        OwnerId = ownerId;
        PetId = petId;
        Date = date;
        Reason = reason;
        Status = status;
    }

    public void UpdateDetails(Guid petId, DateTime date, string reason, AppointmentStatus status)
    {
        if (Status == AppointmentStatus.Completed)
            throw new DomainException("Não é possível alterar um agendamento concluído.");

        ValidateUpdate(petId, date, reason, status);

        PetId = petId;
        Date = date;
        Reason = reason;
        Status = status;
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

    private static void ValidateCreation(Guid petId, Guid ownerId, DateTime date, string reason, AppointmentStatus status)
    {
        if (ownerId == Guid.Empty)
            throw new DomainException("O identificador do tutor é obrigatório.");

        ValidateCommon(petId, date, reason, status);
    }

    private static void ValidateUpdate(Guid petId, DateTime date, string reason, AppointmentStatus status)
    {
        ValidateCommon(petId, date, reason, status);
    }

    private static void ValidateCommon(Guid petId, DateTime date, string reason, AppointmentStatus status)
    {
        if (petId == Guid.Empty)
            throw new DomainException("O identificador do pet é obrigatório.");

        if (date < DateTime.UtcNow)
            throw new DomainException("A data do agendamento não pode ser no passado.");

        if (date > DateTime.UtcNow.AddMonths(6))
            throw new DomainException("A data do agendamento está muito distante.");

        if (!Enum.IsDefined(typeof(AppointmentStatus), status))
            throw new DomainException("Status da consulta inválido.");

        if (!string.IsNullOrWhiteSpace(reason) && reason.Length > 200)
            throw new DomainException("O motivo deve ter no máximo 200 caracteres.");
    }

    private Appointment() { }
}