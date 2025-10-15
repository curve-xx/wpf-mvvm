using System;
using Reservoom.Models;

namespace Reservoom.Services.ReservationConflictValidators;

public interface IReservationConflictValidator
{
    Task<Reservation> GetConflictReservation(Reservation reservation);
}
