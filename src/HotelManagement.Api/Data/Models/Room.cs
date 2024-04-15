using HotelManagement.Api.Data.Common;

namespace HotelManagement.Api.Data.Models;

public class Room
{
    public RoomId Id { get; private set; }
    public HotelId HotelId { get; private set; }
    public RoomNumber RoomNumber { get; private set; }
    public RoomCategory Category { get; private set; }
    public int Capacity { get; private set; }

    public Money Price
    {
        get => new(Amount, Currency);
        private set => (Amount, Currency) = value is Money ? (value.Amount, value.Currency) : (0, Currency.Empty);
    }

    private decimal Amount { get; set; }
    private Currency Currency { get; set; }
    public bool IsAvailable { get; private set; }

    private Room() {}
    public Room(HotelId hotelId, RoomNumber roomNumber, RoomCategory category, int capacity, Money price)
    {
        Id = RoomId.Empty;
        HotelId = hotelId;
        RoomNumber = roomNumber;
        Category = category;
        Capacity = capacity;
        Price = price;
        IsAvailable = true;
    }

    public void ChangeRoomNumber(RoomNumber newRoomNumber) => RoomNumber = newRoomNumber;

    public void ChangeCategory(RoomCategory category) => Category = category;

    public void ResizeCapacity(int capacity) => Capacity = capacity;

    public void AdjustPrice(Money newPrice) => Price = newPrice;

    public void Book() => IsAvailable = false;
}

public readonly record struct RoomId(int Value)
{
    public static RoomId Empty => new(0);
    public static implicit operator int(RoomId roomId) => roomId.Value;
    public override string ToString() => Value.ToString();
}

public record RoomNumber
{
    public int Value { get; }
    public int Floor => int.Parse(Value.ToString("D5")[..2]);
    public int Room => int.Parse(Value.ToString("D5")[2..]);

    public RoomNumber(int roomNumber)
    {
        if (roomNumber <= 0)
        {
            throw new InvalidOperationException("Room number must be greater than zero.");
        }

        Value = roomNumber;
    }
    
    public static implicit operator int(RoomNumber roomNumber) => roomNumber.Value;
}