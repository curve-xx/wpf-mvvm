using System;
using Reservoom.Exceptions;

namespace Reservoom.Models;

public class ReservationBook
{
    private readonly List<Reservation> _reservations;

    public ReservationBook()
    {
        _reservations = new List<Reservation>();
    }

    public IEnumerable<Reservation> GetReservationsForUser(string username)
    {
        return _reservations.Where(r => r.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
    }
    
    public void AddReservation(Reservation reservation)
    {
        foreach (var existingReservation in _reservations)
        {
            if (existingReservation.Conflicts(reservation))
            {
                throw new ReservationConflictException(existingReservation, reservation);
            }
        }

        _reservations.Add(reservation);
    }
}
