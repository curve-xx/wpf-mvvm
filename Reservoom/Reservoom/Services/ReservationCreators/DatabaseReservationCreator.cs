using System;
using Reservoom.DBContexts;
using Reservoom.DTOs;
using Reservoom.Models;

namespace Reservoom.Services.ReservationCreators;

public class DatabaseReservationCreator : IReservationCreator
{
    private readonly ReservoomDbContextFactory _dbContextFactory;

    public DatabaseReservationCreator(ReservoomDbContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task CreateReservation(Reservation reservation)
    {
        using (ReservoomDbContext dbContext = _dbContextFactory.CreateDbContext())
        {
            var reservationDTO = ToReservationDTO(reservation);

            dbContext.Reservations.Add(reservationDTO);
            await dbContext.SaveChangesAsync();
        }
    }

    private ReservationDTO ToReservationDTO(Reservation reservation)
    {
        return new ReservationDTO()
        {
            FloorNumber = reservation.RoomID.FloorNumber,
            RoomNumber = reservation.RoomID.RoomNumber,
            Username = reservation.Username,
            StartDate = reservation.StartDate,
            EndDate = reservation.EndDate
        };
    }
}
