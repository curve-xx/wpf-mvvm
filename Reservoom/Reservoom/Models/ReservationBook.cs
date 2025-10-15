using System;
using Reservoom.Exceptions;
using Reservoom.Services.ReservationConflictValidators;
using Reservoom.Services.ReservationCreators;
using Reservoom.Services.ReservationProviders;

namespace Reservoom.Models;

public class ReservationBook
{
    private readonly IReservationProvider _reservationProvider;
    private readonly IReservationCreator _reservationCreator;
    private readonly IReservationConflictValidator _reservationConflictValidator;

    public ReservationBook(IReservationProvider reservationProvider, IReservationCreator reservationCreator, IReservationConflictValidator reservationConflictValidator)
    {
        _reservationProvider = reservationProvider;
        _reservationCreator = reservationCreator;
        _reservationConflictValidator = reservationConflictValidator;
    }

    public async Task<IEnumerable<Reservation>> GetAllReservations()
    {
        return await _reservationProvider.GetAllReservations();
    }
    
    public async Task AddReservation(Reservation reservation)
    {
        Reservation conflictingReservation = await _reservationConflictValidator.GetConflictReservation(reservation);
       
        if (conflictingReservation != null)
        {
            throw new ReservationConflictException(conflictingReservation, reservation);
        }        

       await _reservationCreator.CreateReservation(reservation);
    }
}
