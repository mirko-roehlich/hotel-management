using HotelManagement.Api.Data.Models;

namespace HotelManagement.Api.API.Dtos;

public record RoomDto(int Id, int RoomNumber, RoomCategory CategoryId, int Capacity, decimal Price)
{
    public static RoomDto From(Room room) =>
        new(room.Id, room.RoomNumber, room.Category, room.Capacity, room.Price);
};

public record CreateRoomRequestDto(int RoomNumber, RoomCategory CategoryId, int Capacity);

