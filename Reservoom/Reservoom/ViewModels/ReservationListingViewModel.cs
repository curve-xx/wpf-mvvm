using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Input;
using Reservoom.Models;

namespace Reservoom.ViewModels;

public class ReservationListingViewModel : ViewModelBase
{
    private readonly ObservableCollection<ReservationViewModel> _reservations;
    public IEnumerable<ReservationViewModel> Reservations => _reservations;

    public ReservationListingViewModel()
    {
        _reservations = new ObservableCollection<ReservationViewModel>();
        _reservations.Add(new ReservationViewModel(new Reservation
        (
            new RoomID(1, 2),
            "John",
            DateTime.Now,
            DateTime.Now
        )));

        _reservations.Add(new ReservationViewModel(new Reservation
        (
            new RoomID(3, 2),
            "Joe",
            DateTime.Now,
            DateTime.Now
        )));

        _reservations.Add(new ReservationViewModel(new Reservation
        (
            new RoomID(2, 4),
            "Marya",
            DateTime.Now,
            DateTime.Now
        )));
    }

    public ICommand MakeReservation { get; }
}
