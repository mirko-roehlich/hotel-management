using HotelManagement.Api.Business.Models;
using HotelManagement.Api.Data.Models;

namespace HotelManagement.Api.Business;

public interface IHotelService
{
    Task<IEnumerable<Hotel>> GetAllHotels();
    Task<Hotel> GetHotelById(HotelId id);
    Task<Hotel> AddHotel(CreateHotelRequest createHotelRequest);
    Task<Hotel> UpdateHotel(HotelId id, UpdateHotelRequest hotel);
    Task DeleteHotel(HotelId id);
}