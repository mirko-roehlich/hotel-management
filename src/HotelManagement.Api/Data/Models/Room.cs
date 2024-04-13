namespace HotelManagement.Api.Data.Models;

public class Room
{
    public RoomId Id { get; set; }
    public HotelId HotelId { get; set; }
    public RoomNumber RoomNumber { get; set; }
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

public record RoomNumber(int Value)
{
    public int Floor => int.Parse(Value.ToString("D5")[..2]);
    public int Room => int.Parse(Value.ToString("D5")[2..]);
    
    public static RoomNumber Create(int roomNumber, int maxFloor)
    {
        if (roomNumber <= 0)
        {
            throw new InvalidOperationException("Room number must be greater than zero.");
        }
        
        var roomString = roomNumber.ToString("D5");
        var floor = int.Parse(roomString[..2]);

        if (floor > maxFloor) throw new ArgumentException("Floor Maximum Exceeded");

        return new RoomNumber(roomNumber);
    }

    public static implicit operator int(RoomNumber roomNumber) => roomNumber.Value;
}