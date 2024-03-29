using HotelManagement.Api.Data.Models;

namespace HotelManagement.Api.API.Dtos;

public record BookedRoomDto(int RoomNumber, decimal Price)
{
    public static BookedRoomDto From(RoomBooking roomBooking) =>
        new(roomBooking.RoomNumber, roomBooking.Price);
}