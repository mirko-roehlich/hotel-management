namespace HotelManagement.Api.Data.Models;

public class RoomBooking
{
    public Guid Id { get; set; }
    public int RoomId { get; set; }
    public int RoomNumber { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; }
}