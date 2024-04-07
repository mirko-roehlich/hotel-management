using HotelManagement.Api.Business.Models;
using HotelManagement.Api.Data.Models;
using HotelManagement.Api.Data.Repositories;

namespace HotelManagement.Api.Business.Implementations;

public class HotelService(IHotelRepository hotelRepository) : IHotelService
{
    public async Task<IEnumerable<Hotel>> GetAllHotels() =>
        await hotelRepository.GetAllHotels();

    public async Task<Hotel> GetHotelById(int id)
    {
        var hotel = await hotelRepository.GetHotelById(id);
        ArgumentNullException.ThrowIfNull(hotel);

        return hotel;
    }

    public async Task<Hotel> AddHotel(CreateHotelRequest createHotelRequest)
    {
        var hotel = new Hotel
        {
            Name = createHotelRequest.Name
        };
        await hotelRepository.AddHotel(hotel);
        return hotel;
    }

    public async Task<Hotel> UpdateHotel(int id, UpdateHotelRequest updateHotelRequest)
    {
        var existingHotel = await hotelRepository.GetHotelById(id);
        if (existingHotel is null)
        {
            throw new Exception();
        }

        existingHotel.Name = updateHotelRequest.Name ?? existingHotel.Name;

        await hotelRepository.UpdateHotel(existingHotel);
        return existingHotel;
    }

    public async Task DeleteHotel(int id)
    {
        var existingHotel = await hotelRepository.GetHotelById(id);
        if (existingHotel is null)
        {
            throw new Exception();
        }

        await hotelRepository.DeleteHotel(existingHotel);
    }
}