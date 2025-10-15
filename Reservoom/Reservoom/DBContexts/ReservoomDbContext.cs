using System;
using Microsoft.EntityFrameworkCore;
using Reservoom.DTOs;

namespace Reservoom.DBContexts;

public class ReservoomDbContext : DbContext
{
    public ReservoomDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ReservationDTO> Reservations { get; set; }
}
