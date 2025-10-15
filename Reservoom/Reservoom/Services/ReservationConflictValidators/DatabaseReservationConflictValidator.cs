using System;
using Microsoft.EntityFrameworkCore;
using Reservoom.DBContexts;
using Reservoom.DTOs;
using Reservoom.Models;

namespace Reservoom.Services.ReservationConflictValidators;

public class DatabaseReservationConflictValidator : IReservationConflictValidator
{
    private readonly ReservoomDbContextFactory _dbContextFactory;

    public DatabaseReservationConflictValidator(ReservoomDbContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<Reservation> GetConflictReservation(Reservation reservation)
    {
        using (var dbContext = _dbContextFactory.CreateDbContext())
        {
            ReservationDTO? reservationDTO = await dbContext.Reservations
                .AsNoTracking()
                .Where(r => r.FloorNumber == reservation.RoomID.FloorNumber)
                .Where(r => r.RoomNumber == reservation.RoomID.RoomNumber)
                .Where(r => r.StartDate < reservation.EndDate)
                .Where(r => reservation.StartDate < r.EndDate)
                .FirstOrDefaultAsync();

            if (reservationDTO == null)
            {
                return null;
            }

            return ToReservation(reservationDTO);
        }
    }

    private static Reservation ToReservation(ReservationDTO r)
    {
        return new Reservation(new RoomID(r.FloorNumber, r.RoomNumber), r.Username, r.StartDate, r.EndDate);
    }
}
