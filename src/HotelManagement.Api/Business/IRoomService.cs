using HotelManagement.Api.Business.Models;
using HotelManagement.Api.Data.Models;

namespace HotelManagement.Api.Business;

public interface IRoomService
{
    Task<IEnumerable<Room>> GetAllRooms(HotelId hotelId);
    Task<Room> GetRoomById(HotelId hotelId, int roomId);
    Task<Room> AddRoom(HotelId hotelId, CreateRoomRequest room);
    
    Task<Room> UpdateRoom(HotelId hotelId, int roomId, UpdateRoomRequest room);
    Task DeleteRoom(HotelId hotelId, int roomId);
    Task<IEnumerable<Room>> GetAvailableRooms(HotelId hotelId);
}