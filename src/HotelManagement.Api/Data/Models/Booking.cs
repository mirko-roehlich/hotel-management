namespace HotelManagement.Api.Data.Models;

public class Booking
{
    public Guid Id { get; set; }
    public string BookingNumber { get; set; } = string.Empty;

    public int HotelId { get; set; }
    public ICollection<RoomBooking> RoomBookings { get; set; } = [];
    private const decimal Seed = 0m;
    public decimal TotalAmount => RoomBookings.Aggregate(Seed, (total, roomBooking) => total + roomBooking.Price);
}