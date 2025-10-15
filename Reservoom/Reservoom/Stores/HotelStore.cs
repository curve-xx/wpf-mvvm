using System;
using Reservoom.Models;

namespace Reservoom.Stores;

public class HotelStore
{
    private readonly Hotel _hotel;
    private readonly List<Reservation> _reservations;
    private Lazy<Task> _initializeLazy;
    
    public IEnumerable<Reservation> Reservations => _reservations;
    public event Action<Reservation> ReservationsMade;

    public HotelStore(Hotel hotel)
    {
        _hotel = hotel;
        _initializeLazy = new Lazy<Task>(Initailize());

        _reservations = new List<Reservation>();
    }

    public async Task Load()
    {
        try
        {
            await _initializeLazy.Value;
        }
        catch (Exception)
        {
            _initializeLazy = new Lazy<Task>(Initailize());
        }
    }

    public async Task MakeReservation(Reservation reservation)
    {
        await _hotel.MakeReservation(reservation);

        _reservations.Add(reservation);

        OnReservationMade(reservation);
    }

    private void OnReservationMade(Reservation reservation)
    {
        ReservationsMade?.Invoke(reservation);
    }

    private async Task Initailize()
    {
        IEnumerable<Reservation> reservations = await _hotel.GetAllReservations();

        _reservations.Clear();
        _reservations.AddRange(reservations);
    }
}
