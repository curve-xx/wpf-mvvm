using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Input;
using Reservoom.Commamds;
using Reservoom.Models;
using Reservoom.Services;
using Reservoom.Stores;

namespace Reservoom.ViewModels;

public class ReservationListingViewModel : ViewModelBase
{
    private readonly Hotel _hotel;
    private readonly ObservableCollection<ReservationViewModel> _reservations;
    public IEnumerable<ReservationViewModel> Reservations => _reservations;

    public ReservationListingViewModel(Hotel hotel, NavigationService makeReservationNavigationService)
    {
        _hotel = hotel;
        _reservations = new ObservableCollection<ReservationViewModel>();

        MakeReservationCommand = new NavigateCommand(makeReservationNavigationService);

        UpdateReservations();
    }

    public ICommand MakeReservationCommand { get; }

    private void UpdateReservations()
    {
        _reservations.Clear();
        foreach (Reservation reservation in _hotel.GetAllReservations())
        {
            ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
            _reservations.Add(reservationViewModel);
        }
    }
}
