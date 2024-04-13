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

        modelBuilder.Entity<Room>()
            .Property(e => e.Id)
            .HasConversion<RoomIdConverter>()
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Booking>()
            .Property(e => e.Id)
            .HasConversion<BookingIdConverter>();
        modelBuilder.Entity<Booking>()
            .Property(e => e.HotelId)
            .HasConversion<HotelIdConverter>();

        modelBuilder.Entity<Booking>()
            .Ignore(e => e.TotalAmount);

        modelBuilder.Entity<Room>()
            .Property(e => e.RoomNumber)
            .HasConversion<RoomNumberConverter>();

        modelBuilder.Entity<RoomBooking>()
            .Property(e => e.RoomNumber)
            .HasConversion<RoomNumberConverter>();

        modelBuilder.Entity<RoomBooking>()
            .Property(e => e.RoomId)
            .HasConversion<RoomIdConverter>();
    }
}

public class HotelIdConverter() : ValueConverter<HotelId, int>(id => id.Value, value => new HotelId(value));

public class RoomIdConverter() : ValueConverter<RoomId, int>(id => id.Value, value => new RoomId(value));

public class BookingIdConverter() : ValueConverter<BookingId, Guid>(id => id.Value, value => new BookingId(value));

public class RoomNumberConverter() : ValueConverter<RoomNumber, int>(number => number.Value, value => new RoomNumber(value));