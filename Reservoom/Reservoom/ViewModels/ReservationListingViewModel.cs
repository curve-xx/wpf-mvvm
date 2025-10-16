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

    private string _errorMessage;
    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            if (_errorMessage != value)
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }
    }

    public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            if (_isLoading != value)
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
    }

    public ReservationListingViewModel(HotelStore hotelStore,
        NavigationService<MakeReservationViewModel> makeReservationNavigationService)
    {
        _hotelStore = hotelStore;
        _reservations = new ObservableCollection<ReservationViewModel>();

        LoadReservationCommand = new LoadReservationsCommand(this, hotelStore);
        MakeReservationCommand = new NavigateCommand<MakeReservationViewModel>(makeReservationNavigationService);

        _hotelStore.ReservationMade += OnReservationMade;
    }

    // public override void Dispose()
    // {
    //     _hotelStore.ReservationMade -= OnReservationMade;
    //     base.Dispose();
    // }

    private void OnReservationMade(Reservation reservation)
    {
        ReservationViewModel reservationViewModel = new ReservationViewModel(reservation);
        _reservations.Add(reservationViewModel);
    }

    public MakeReservationViewModel MakeReservationViewModel { get; }

    public ICommand LoadReservationCommand { get; }
    public ICommand MakeReservationCommand { get; }

    public static ReservationListingViewModel LoadViewModel(HotelStore hotelStore,
        NavigationService<MakeReservationViewModel> makeReservationNavigationService)
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
