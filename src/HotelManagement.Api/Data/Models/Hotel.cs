namespace HotelManagement.Api.Data.Models;

public class Hotel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IList<Room> Rooms { get; set; } = [];
}