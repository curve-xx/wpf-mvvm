using System.Configuration;
using System.Data;
using System.Windows;
using Reservoom.Exceptions;
using Reservoom.Models;

namespace Reservoom;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        Hotel hotel = new Hotel("Grand Hotel");
        try
        {
            hotel.MakeReservation(new Reservation(
                new RoomID(1, 3),
                "John Smith",
                new DateTime(2000, 1, 1),
                new DateTime(2000, 1, 2)));
            hotel.MakeReservation(new Reservation(
                new RoomID(1, 3),
                "Jane Doe",
                new DateTime(2000, 1, 1),
                new DateTime(2000, 1, 4)));
        }
        catch (ReservationConflictException)
        {
            
        }
        base.OnStartup(e);        
    }
}

