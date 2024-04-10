using HotelManagement.Api.Data.Common;
using HotelManagement.Api.Data.Models;
using Microsoft.EntityFrameworkCore;

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
        
        modelBuilder.Entity<Room>().Ignore(e => e.Price);

        modelBuilder.Entity<Room>()
            .Property<decimal>("Amount")
            .HasColumnName("Price")
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<Room>()
            .Property<Currency>("Currency")
            .HasColumnName("Currency")
            .HasConversion(
                currency => currency.Symbol,
                symbol => new Currency(symbol)
            );

        modelBuilder.Entity<RoomBooking>().Ignore(e => e.Price);

        modelBuilder.Entity<RoomBooking>()
            .Property<decimal>("Amount")
            .HasColumnName("Price");

        modelBuilder.Entity<RoomBooking>()
            .Property<Currency>("Currency")
            .HasConversion(
                currency => currency.Symbol,
                symbol => new Currency(symbol)
            );
    }
}