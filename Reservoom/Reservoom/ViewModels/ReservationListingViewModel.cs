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
    private readonly HotelStore _hotelStore;
    private readonly ObservableCollection<ReservationViewModel> _reservations;

    public IEnumerable<ReservationViewModel> Reservations => _reservations;

    public ReservationListingViewModel(HotelStore hotelStore,
        NavigationService makeReservationNavigationService)
    {
        _hotelStore = hotelStore;
        _reservations = new ObservableCollection<ReservationViewModel>();

        LoadReservationCommand = new LoadReservationsCommand(this, hotelStore);
        MakeReservationCommand = new NavigateCommand(makeReservationNavigationService);

        _hotelStore.ReservationsMade += OnReservationMade;
    }

    public override void Dispose()
    {
        _hotelStore.ReservationsMade -= OnReservationMade;
        base.Dispose();
    }

    private void OnReservationMade(Reservation reservation)
    {
        ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
        _reservations.Add(reservationViewModel);
    }

    public MakeReservationViewModel MakeReservationViewModel { get; }

    public ICommand LoadReservationCommand { get; }
    public ICommand MakeReservationCommand { get; }

    public static ReservationListingViewModel LoadViewModel(HotelStore hotelStore,
        NavigationService makeReservationNavigationService)
    {
        ReservationListingViewModel viewModel = new ReservationListingViewModel(hotelStore, makeReservationNavigationService);
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
