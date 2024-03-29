using HotelManagement.Api.Data.Models;

namespace HotelManagement.Api.API.Dtos;

public record RoomRequestDto(int NumberOfRooms, RoomCategory Category);