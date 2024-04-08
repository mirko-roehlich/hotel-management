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
        modelBuilder.Entity<Hotel>()
            .HasKey(e => e.Id);
            
        modelBuilder.Entity<Hotel>()
            .Property(e => e.Id)
            .HasConversion<HotelIdConverter>()
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Room>()
            .Property(e => e.HotelId)
            .HasConversion<HotelIdConverter>();

        modelBuilder.Entity<Booking>()
            .Property(e => e.HotelId)
            .HasConversion<HotelIdConverter>();

        modelBuilder.Entity<Booking>()
            .Ignore(e => e.TotalAmount);
    }
}

public class HotelIdConverter() : ValueConverter<HotelId, int>(id => id.Value, value => new HotelId(value));
