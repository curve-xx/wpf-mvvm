using System;
using Reservoom.Models;

namespace Reservoom.Stores;

public class HotelStore
{
    private readonly Hotel _hotel;
    private readonly List<Reservation> _reservations;
    private Lazy<Task> _initializeLazy;

    public IEnumerable<Reservation> Reservations => _reservations;
    public event Action<Reservation>? ReservationMade;

    public HotelStore(Hotel hotel)
    {
        _hotel = hotel;
        _reservations = new List<Reservation>();

        _initializeLazy = new Lazy<Task>(() => Initialize());
    }

    public async Task Load()
    {
        try
        {
            await _initializeLazy.Value;
        }
        catch (Exception)
        {
            // If initialization failed, reset so next Load() retries
            _initializeLazy = new Lazy<Task>(() => Initialize());
            throw;
        }
    }

    public async Task MakeReservation(Reservation reservation)
    {
        await _hotel.MakeReservation(reservation);

        _reservations.Add(reservation);

        OnReservationsMade(reservation);
    }

    private void OnReservationsMade(Reservation reservation)
    {
        ReservationMade?.Invoke(reservation);
    }

    private async Task Initialize()
    {
        IEnumerable<Reservation> reservations = await _hotel.GetAllReservations();

        _reservations.Clear();
        _reservations.AddRange(reservations);
    }
}
