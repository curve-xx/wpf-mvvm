using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Reservoom.DBContexts;
using Reservoom.Exceptions;
using Reservoom.Models;
using Reservoom.Services;
using Reservoom.Services.ReservationConflictValidators;
using Reservoom.Services.ReservationCreators;
using Reservoom.Services.ReservationProviders;
using Reservoom.Stores;
using Reservoom.ViewModels;

namespace Reservoom;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private const string CONNECTION_STRING = "Data Source=reservoom.db";
    private readonly Hotel _hotel;
    private readonly HotelStore _hotelStore;
    private readonly NavigationStore _navigationStore;
    private ReservoomDbContextFactory _reservoomDbContextFactory;

    public App()
    {
        _reservoomDbContextFactory = new ReservoomDbContextFactory(CONNECTION_STRING);
        IReservationProvider reservationProvider = new DatabaseReservationProvider(_reservoomDbContextFactory);
        IReservationCreator reservationCreator = new DatabaseReservationCreator(_reservoomDbContextFactory);
        IReservationConflictValidator reservationValidator = new DatabaseReservationConflictValidator(_reservoomDbContextFactory);

        ReservationBook reservationBook = new ReservationBook(reservationProvider, reservationCreator, reservationValidator);
        _hotel = new Hotel("The Grand", reservationBook);
        _hotelStore = new HotelStore(_hotel);
        _navigationStore = new NavigationStore();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        using (ReservoomDbContext dbContext = _reservoomDbContextFactory.CreateDbContext())
        {
            dbContext.Database.Migrate();
        }

        _navigationStore.CurrentViewModel = CreateReservationViewModel();
        MainWindow = new MainWindow()
        {
            DataContext = new MainViewModel(_navigationStore)
        };
        MainWindow.Show();

        base.OnStartup(e);
    }

    private MakeReservationViewModel CreateMakeReservationViewModel()
    {
        return new MakeReservationViewModel(_hotelStore, new NavigationService(_navigationStore, CreateReservationViewModel));
    }

    private ReservationListingViewModel CreateReservationViewModel()
    {
        return ReservationListingViewModel.LoadViewModel(_hotelStore, CreateMakeReservationViewModel(), new NavigationService(_navigationStore, CreateMakeReservationViewModel));
    }
}

