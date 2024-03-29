using HotelManagement.Api.Data.Models;

namespace HotelManagement.Api.Data.Repositories;

public interface IRoomRepository
{
    Task AddRoom(Room room);
    Task DeleteRoom(Room room);
    Task<List<Room>> GetAllRooms(int hotelId);
    Task<Room?> GetRoomById(int hotelId, int id);
    Task UpdateRoom(Room room);
    Task<IEnumerable<Room>> GetAvailableRooms(int hotelId);
}