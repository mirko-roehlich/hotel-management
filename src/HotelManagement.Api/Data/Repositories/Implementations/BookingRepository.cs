using HotelManagement.Api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Api.Data.Repositories.Implementations;

internal class BookingRepository(AppDbContext dbContext) : IBookingRepository
{
    public async Task Add(Booking booking)
    {
        await dbContext.Bookings.AddAsync(booking);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Booking?> GetById(BookingId id) =>
        await dbContext.Bookings
            .Include(b => b.RoomBookings)
            .SingleOrDefaultAsync(b => b.Id == id);
}