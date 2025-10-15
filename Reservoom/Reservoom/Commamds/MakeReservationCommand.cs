using System;
using System.ComponentModel;
using System.Windows;
using Reservoom.Exceptions;
using Reservoom.Models;
using Reservoom.Services;
using Reservoom.Stores;
using Reservoom.ViewModels;

namespace Reservoom.Commamds;

public class MakeReservationCommand : AsyncCommandBase
{
    private readonly MakeReservationViewModel _makeReservationViewModel;
    private readonly HotelStore _hotelStore;
    private readonly NavigationService _reservationViewNavigationService;

    public MakeReservationCommand(MakeReservationViewModel makeReservationViewModel,
        HotelStore hotelStore,
        NavigationService reservationViewNavigationService)
    {
        _makeReservationViewModel = makeReservationViewModel;
        _hotelStore = hotelStore;
        _reservationViewNavigationService = reservationViewNavigationService;
        _makeReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        Reservation reservation = new Reservation(
            new RoomID(_makeReservationViewModel.FloorNumber, _makeReservationViewModel.RoomNumber),
            _makeReservationViewModel.Username,
            _makeReservationViewModel.StartDate,
            _makeReservationViewModel.EndDate
        );

        try
        {
            await _hotelStore.MakeReservation(reservation);
            MessageBox.Show("Successfully reserved room.", "Success",
                MessageBoxButton.OK, MessageBoxImage.Information);

            _reservationViewNavigationService.Navigate();
        }
        catch (ReservationConflictException)
        {
            MessageBox.Show("The room is already taken.", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception)
        {
            MessageBox.Show("Failed to make reservation.", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return !string.IsNullOrEmpty(_makeReservationViewModel.Username) &&
            _makeReservationViewModel.FloorNumber > 0 &&
            _makeReservationViewModel.RoomNumber > 0 &&
            base.CanExecute(parameter);
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(MakeReservationViewModel.Username) ||
            e.PropertyName == nameof(MakeReservationViewModel.FloorNumber) ||
            e.PropertyName == nameof(MakeReservationViewModel.RoomNumber))
        {
            OnCanExecuteChanged();
        }
    }
}
