namespace HotelManagement.Api.Data.Models;

public class Room
{
    public RoomId Id { get; set; }
    public HotelId HotelId { get; set; }
    public int RoomNumber { get; set; }
    public RoomCategory Category { get; set; }
    public int Capacity { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
}

public readonly record struct RoomId(int Value)
{
    public static implicit operator int(RoomId roomId) => roomId.Value;
    public override string ToString() => Value.ToString();
}