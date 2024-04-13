namespace HotelManagement.Api.Data.Models;

public class RoomBooking
{
    public Guid Id { get; set; }
    public RoomId RoomId { get; set; }
    public RoomNumber RoomNumber { get; set; }
    public decimal Price { get; set; }
}