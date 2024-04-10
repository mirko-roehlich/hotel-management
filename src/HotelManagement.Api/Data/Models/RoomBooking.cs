using HotelManagement.Api.Data.Common;

namespace HotelManagement.Api.Data.Models;

public class RoomBooking
{
    public Guid Id { get; set; }
    public int RoomId { get; set; }
    public int RoomNumber { get; set; }
    public Money Price
    {
        get => new(Amount, Currency);
        set => (Amount, Currency) = value is Money ? (value.Amount, value.Currency) : (0, Currency.Empty);
    }
    
    private decimal Amount { get; set; }
    private Currency Currency { get; set; }
}