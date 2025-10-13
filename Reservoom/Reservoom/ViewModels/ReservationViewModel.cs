using System;
using Reservoom.Models;

namespace Reservoom.ViewModels;

public class ReservationViewModel : ViewModelBase
{
    private readonly Reservation _reservation;

    public ReservationViewModel(Reservation reservation)
    {
        _reservation = reservation;
    }
   
    public string? RoomID => _reservation.RoomID?.ToString();
    public string Username => _reservation.Username;
    public string StartDate => _reservation.StartDate.ToString("d");
    public string EndDate => _reservation.EndDate.ToString("d");
}
