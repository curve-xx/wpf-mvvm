using System;

namespace Reservoom.Models;

public class Reservation
{
    public Reservation(RoomID roomID, string username, DateTime startDate, DateTime endDate)
    {
        RoomID = roomID;
        Username = username;
        StartDate = startDate;
        EndDate = endDate;
    }

    public RoomID RoomID { get; }
    public string Username { get; }
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }

    public TimeSpan Length => EndDate.Subtract(StartDate);

    internal bool Conflicts(Reservation reservation)
    {
        if (!RoomID.Equals(reservation.RoomID))
        {
            return false;
        }
        
        return StartDate < reservation.EndDate && reservation.StartDate < EndDate;
    }
}
