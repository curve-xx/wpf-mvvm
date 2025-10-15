using System;
using Microsoft.EntityFrameworkCore;
using Reservoom.DBContexts;
using Reservoom.DTOs;
using Reservoom.Models;

namespace Reservoom.Services.ReservationProviders;

public class DatabaseReservationProvider : IReservationProvider
{
    private readonly ReservoomDbContextFactory _dbContextFactory;

    public DatabaseReservationProvider(ReservoomDbContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<IEnumerable<Reservation>> GetAllReservations()
    {
        using (ReservoomDbContext dbContext = _dbContextFactory.CreateDbContext())
        {
            IEnumerable<ReservationDTO> reservationDTOs = await dbContext.Reservations.ToListAsync();
            return reservationDTOs.Select(r => ToReservation(r));
        }
    }

    private static Reservation ToReservation(ReservationDTO r)
    {
        return new Reservation(new RoomID(r.FloorNumber, r.RoomNumber), r.Username, r.StartDate, r.EndDate);
    }
}
