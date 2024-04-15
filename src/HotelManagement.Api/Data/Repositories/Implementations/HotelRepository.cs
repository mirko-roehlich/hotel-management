using HotelManagement.Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Api.Data.Repositories.Implementations;

public class HotelRepository(AppDbContext dbContext) : IHotelRepository
{
    public async Task AddHotel(Hotel hotel)
    {
        ArgumentNullException.ThrowIfNull(hotel);

        await dbContext.Hotels.AddAsync(hotel);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteHotel(Hotel hotel)
    {
        dbContext.Hotels.Remove(hotel);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<Hotel>> GetAllHotels() =>
        await dbContext.Hotels.ToListAsync();

    public async Task<Hotel?> GetHotelById(HotelId id) =>
        await dbContext.Hotels
            .Include(h => h.Rooms)
            .FirstOrDefaultAsync(h => h.Id == id);

    public async Task UpdateHotel(Hotel hotel)
    {
        ArgumentNullException.ThrowIfNull(hotel);

        await dbContext.SaveChangesAsync();
    }

    public async Task Save() => 
        await dbContext.SaveChangesAsync();
}