using HotelManagement.Api.Data.Models;

namespace HotelManagement.Api.Business.Models;

public record CreateRoomRequest(int RoomNumber, RoomCategory Category, int Capacity);