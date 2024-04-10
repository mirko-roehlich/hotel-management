using HotelManagement.Api.Data.Common;

namespace HotelManagement.Api.Data.Models;

public class Room
{
    public int Id { get; set; }
    public int HotelId { get; set; }
    public int RoomNumber { get; set; }
    public RoomCategory Category { get; set; }
    public int Capacity { get; set; }

    public Money Price
    {
        get => new(Amount, Currency);
        set => (Amount, Currency) = value is Money ? (value.Amount, value.Currency) : (0, Currency.Empty);
    }

    private decimal Amount { get; set; }
    private Currency Currency { get; set; }
    public bool IsAvailable { get; set; }
}