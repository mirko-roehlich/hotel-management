using HotelManagement.Api.Data.Models;

namespace HotelManagement.Api.Data.Repositories;

public interface IBookingRepository
{
    Task Add(Booking booking);
    Task<Booking?> GetById(Guid id);
}