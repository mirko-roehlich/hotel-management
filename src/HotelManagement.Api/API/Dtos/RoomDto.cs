using HotelManagement.Api.Data.Models;

namespace HotelManagement.Api.API.Dtos;

public record RoomDto(int Id, int RoomNumber, RoomCategory CategoryId, int Capacity, decimal Price, string Currency)
{
    public static RoomDto From(Room room) =>
        new(room.Id, room.RoomNumber, room.Category, room.Capacity, room.Price.Amount, room.Price.Currency.Symbol);
};

public record CreateRoomRequestDto(int RoomNumber, RoomCategory CategoryId, int Capacity, decimal Price, string Currency);

public record UpdateRoomRequestDto(int? RoomNumber, RoomCategory? CategoryId, int? Capacity, decimal? Price, string? Currency);
