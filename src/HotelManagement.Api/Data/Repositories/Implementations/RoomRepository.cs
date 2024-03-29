using HotelManagement.Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Api.Data.Repositories.Implementations;

public class RoomRepository(AppDbContext dbContext) : IRoomRepository
{
    public async Task AddRoom(Room hotel)
    {
        ArgumentNullException.ThrowIfNull(hotel);

        await dbContext.Rooms.AddAsync(hotel);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteRoom(Room hotel)
    {
        dbContext.Rooms.Remove(hotel);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<Room>> GetAllRooms(int hotelId) =>
        await dbContext.Rooms
            .Where(r => r.HotelId == hotelId)
            .ToListAsync();

    public async Task<Room?> GetRoomById(int hotelId, int id) =>
        await dbContext.Rooms.FirstOrDefaultAsync(r => r.Id == id && r.HotelId == hotelId);

    public async Task UpdateRoom(Room room)
    {
        ArgumentNullException.ThrowIfNull(room);

        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Room>> GetAvailableRooms(int hotelId) =>
        await dbContext.Rooms
            .Where(r => r.HotelId == hotelId && r.IsAvailable)
            .ToListAsync();
}