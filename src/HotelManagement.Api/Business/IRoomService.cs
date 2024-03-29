using HotelManagement.Api.Business.Models;
using HotelManagement.Api.Data.Models;

namespace HotelManagement.Api.Business;

public interface IRoomService
{
    Task<IEnumerable<Room>> GetAllRooms(int hotelId);
    Task<Room> GetRoomById(int hotelId, int roomId);
    Task<Room> AddRoom(int hotelId, CreateRoomRequest room);
    Task<Room> UpdateRoom(int hotelId, int roomId, UpdateRoomRequest room);
    Task DeleteRoom(int hotelId, int roomId);
    Task<IEnumerable<Room>> GetAvailableRooms(int hotelId);
}