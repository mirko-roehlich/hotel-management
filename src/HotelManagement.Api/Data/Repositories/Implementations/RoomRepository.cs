using HotelManagement.Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Api.Data.Repositories.Implementations;

public class RoomRepository(AppDbContext dbContext) : IRoomRepository
{
    public async Task AddRoom(Room room)
    {
        ArgumentNullException.ThrowIfNull(room);
        
        await dbContext.Rooms.AddAsync(room);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteRoom(Room room)
    {
        dbContext.Rooms.Remove(room);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<Room>> GetAllRooms(HotelId hotelId) =>
        await dbContext.Rooms
            .Where(r => r.HotelId == hotelId)
            .ToListAsync();

    public async Task<Room?> GetRoomById(HotelId hotelId, RoomId id) =>
        await dbContext.Rooms.FirstOrDefaultAsync(r => r.Id == id && r.HotelId == hotelId);

    public async Task UpdateRoom(Room room)
    {
        ArgumentNullException.ThrowIfNull(room);

        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Room>> GetAvailableRooms(HotelId hotelId) =>
        await dbContext.Rooms
            .Where(r => r.HotelId == hotelId && r.IsAvailable)
            .ToListAsync();
}