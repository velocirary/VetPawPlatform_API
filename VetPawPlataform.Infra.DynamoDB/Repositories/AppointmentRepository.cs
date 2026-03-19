using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using VetPawPlatform.Domain.Entities;
using VetPawPlatform.Domain.Enums;
using VetPawPlatform.Domain.Exceptions;
using VetPawPlatform.Domain.Interfaces;
using VetPawPlatform.Infra.DynamoDB.Models;
using VetPawPlatform.Infra.Models;

namespace VetPawPlatform.Infra.DynamoDB.Repositories;

public class AppointmentRepository(IDynamoDBContext context) : IAppointmentRepository
{
    private readonly IDynamoDBContext _context = context;

    public async Task CreateAsync(Appointment Appointment)
    {
        var dbModel = MapToDbModel(Appointment);
        await _context.SaveAsync(dbModel);
    }

    public async Task UpdateAsync(Appointment Appointment)
    {
        var dbModel = MapToDbModel(Appointment);
        await _context.SaveAsync(dbModel);
    }

    public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
    {
        var dbModels = await _context.ScanAsync<AppointmentDbModel>([]).GetRemainingAsync();
        return dbModels.Select(MapToDomain);
    }

    public async Task<Appointment?> GetByIdAsync(Guid id)
    {
        var AppointmentDb = await _context.LoadAsync<AppointmentDbModel>(id);
        return AppointmentDb == null ? null : MapToDomain(AppointmentDb);
    }

    private static Appointment MapToDomain(AppointmentDbModel dbModel)
    {
        if (!Enum.TryParse<AppointmentStatus>(dbModel.Status, out var status))
            throw new DomainException("Status da consulta inválido.");

        return Appointment.Rehydrate(
            dbModel.Id,
            dbModel.OwnerId,
            dbModel.PetId,
            dbModel.Date,
            dbModel.Reason,
            status
         );
    }

    private static AppointmentDbModel MapToDbModel(Appointment Appointment) => new()
    {
        Id = Appointment.Id,
        OwnerId = Appointment.OwnerId,
        PetId = Appointment.PetId,
        Date = Appointment.Date,
        Reason = Appointment.Reason ?? string.Empty,
        Status = Appointment.Status.ToString()
    };
}