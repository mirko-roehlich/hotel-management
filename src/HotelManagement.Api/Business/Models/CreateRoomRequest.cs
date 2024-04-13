using HotelManagement.Api.Data.Common;
using HotelManagement.Api.Data.Models;

namespace HotelManagement.Api.Business.Models;

public record CreateRoomRequest
{
    public int RoomNumber { get; }
    public RoomCategory Category { get; }
    
    public int Capacity { get; }
    
    public Money Price { get; }

    public CreateRoomRequest(int roomNumber, RoomCategory category, int capacity, decimal amount, string currency)
    {
        RoomNumber = roomNumber;
        Category = category;
        Capacity = capacity;
        Price = new Money(amount, new Currency(currency));
    }
}