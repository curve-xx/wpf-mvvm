using System;
using System.Windows;
using Reservoom.Models;
using Reservoom.Stores;
using Reservoom.ViewModels;

namespace Reservoom.Commamds;

public class LoadReservationsCommand : AsyncCommandBase
{
    private readonly ReservationListingViewModel _viewModel;
    private readonly HotelStore _hotelStore;

    public LoadReservationsCommand(ReservationListingViewModel viewModel, HotelStore hotelStore)
    {
        _viewModel = viewModel;
        _hotelStore = hotelStore;
    }

    public override async Task ExecuteAsync(object? parameter)
    {
        _viewModel.IsLoading = true;

        try
        {
            await _hotelStore.Load();
            _viewModel.UpdateReservations(_hotelStore.Reservations);
        }
        catch (Exception)
        {
            MessageBox.Show("Failed to load reservations.", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        _viewModel.IsLoading = false;
    }
}
