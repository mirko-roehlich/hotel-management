using HotelManagement.Api.Data.Models;

namespace HotelManagement.Api.API.Dtos;

public record BookedRoomDto(int RoomNumber, decimal Price, string Currency)
{
    public static BookedRoomDto From(RoomBooking roomBooking) =>
        new(roomBooking.RoomNumber, roomBooking.Price.Amount, roomBooking.Price.Currency.Symbol);
}