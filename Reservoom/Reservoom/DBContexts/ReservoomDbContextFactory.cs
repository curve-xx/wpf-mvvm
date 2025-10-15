using System;
using Microsoft.EntityFrameworkCore;

namespace Reservoom.DBContexts;

public class ReservoomDbContextFactory
{
    private string _connectionString;

    public ReservoomDbContextFactory(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public ReservoomDbContext CreateDbContext()
    {
        DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;
        return new ReservoomDbContext(options);
    }

}
