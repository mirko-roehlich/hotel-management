using HotelManagement.Api.Business.Models;
using HotelManagement.Api.Data.Models;

namespace HotelManagement.Api.Business;

public interface IRoomService
{
    Task<IEnumerable<Room>> GetAllRooms(HotelId hotelId);
    Task<Room> GetRoomById(HotelId hotelId, RoomId roomId);
    Task<Room> AddRoom(HotelId hotelId, CreateRoomRequest room);
    
    Task<Room> UpdateRoom(HotelId hotelId, RoomId roomId, UpdateRoomRequest room);
    Task DeleteRoom(HotelId hotelId, RoomId roomId);
    Task<IEnumerable<Room>> GetAvailableRooms(HotelId hotelId);
}