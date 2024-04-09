using HotelManagement.Api.Data.Models;

namespace HotelManagement.Api.Data.Repositories;

public interface IRoomRepository
{
    Task AddRoom(Room room);
    Task DeleteRoom(Room room);
    Task<List<Room>> GetAllRooms(HotelId hotelId);
    Task<Room?> GetRoomById(HotelId hotelId, RoomId id);
    Task UpdateRoom(Room room);
    Task<IEnumerable<Room>> GetAvailableRooms(HotelId hotelId);
}