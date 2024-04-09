using HotelManagement.Api.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelManagement.Api.Data;

public class AppDbContext : DbContext
{
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>()
            .Ignore(e => e.TotalAmount);

        modelBuilder.Entity<Room>()
            .Property(e => e.RoomNumber)
            .HasConversion<RoomNumberConverter>();

        modelBuilder.Entity<RoomBooking>()
            .Property(e => e.RoomNumber)
            .HasConversion<RoomNumberConverter>();
    }
}

public class RoomNumberConverter() : ValueConverter<RoomNumber, int>(number => number.Value, value => new RoomNumber(value));