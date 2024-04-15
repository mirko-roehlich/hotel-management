using HotelManagement.Api.Data.Models;

namespace HotelManagement.Api.Data.Repositories;

public interface IHotelRepository
{
    Task AddHotel(Hotel hotel);
    Task DeleteHotel(Hotel hotel);
    Task<List<Hotel>> GetAllHotels();
    Task<Hotel?> GetHotelById(HotelId id);
    Task UpdateHotel(Hotel hotel);
    Task Save();
}