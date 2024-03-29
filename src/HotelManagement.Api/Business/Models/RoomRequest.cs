using HotelManagement.Api.Data.Models;

namespace HotelManagement.Api.Business.Models;

public record RoomRequest(int NumberOfRooms, RoomCategory Category);