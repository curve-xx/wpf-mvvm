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

        LoadReservationCommand = new LoadReservationsCommand(this, _hotel);
        MakeReservationCommand = new NavigateCommand(makeReservationNavigationService);
    }

    public ICommand LoadReservationCommand { get; }
    public ICommand MakeReservationCommand { get; }

    public static ReservationListingViewModel LoadViewModel(Hotel hotel, NavigationService makeReservationNavigationService)
    {
        ReservationListingViewModel viewModel = new ReservationListingViewModel(hotel, makeReservationNavigationService);
        viewModel.LoadReservationCommand.Execute(null);
        return viewModel;
    }

    public void UpdateReservations(IEnumerable<Reservation> reservations)
    {
        _reservations.Clear();
        foreach (Reservation reservation in reservations)
        {
            ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
            _reservations.Add(reservationViewModel);
        }
    }
}
