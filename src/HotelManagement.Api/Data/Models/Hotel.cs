namespace HotelManagement.Api.Data.Models;

public class Hotel
{
    public HotelId Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IList<Room> Rooms { get; set; } = [];
}

public readonly record struct HotelId(int Value)
{
    public static implicit operator int(HotelId hotelId) => hotelId.Value;
    public override string ToString() => Value.ToString();
}