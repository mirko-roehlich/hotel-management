using HotelManagement.Api.Data.Models;

namespace HotelManagement.Api.Business.Models;

public record UpdateRoomRequest(int? RoomNumber, RoomCategory? Category, int? Capacity, decimal? Price, string? Currency);
