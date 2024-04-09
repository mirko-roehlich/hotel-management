namespace HotelManagement.Api.Data.Models;

public class Booking
{
    public BookingId Id { get; set; }
    public string BookingNumber { get; set; } = string.Empty;

    public HotelId HotelId { get; set; }
    public ICollection<RoomBooking> RoomBookings { get; set; } = [];
    private const decimal Seed = 0m;
    public decimal TotalAmount => RoomBookings.Aggregate(Seed, (total, roomBooking) => total + roomBooking.Price);
}

public readonly record struct BookingId(Guid Value)
{
    private const string Prefix = "booking_";
    public static BookingId Create() => new(Guid.NewGuid());
    public static implicit operator Guid(BookingId bookingId) => bookingId.Value;
    public override string ToString() => $"{Prefix}{Value.ToString()}";

    public static BookingId TryParse(string? value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        
        var foo = value[Prefix.Length..];
        var id = Guid.Parse(foo);

        return new BookingId(id);
    }
}