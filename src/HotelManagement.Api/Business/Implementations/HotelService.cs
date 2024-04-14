using HotelManagement.Api.Business.Models;
using HotelManagement.Api.Data.Models;
using HotelManagement.Api.Data.Repositories;

namespace HotelManagement.Api.Business.Implementations;

public class HotelService(IHotelRepository hotelRepository) : IHotelService
{
    public async Task<IEnumerable<Hotel>> GetAllHotels() =>
        await hotelRepository.GetAllHotels();

    public async Task<Hotel> GetHotelById(HotelId id)
    {
        var hotel = await hotelRepository.GetHotelById(id);
        ArgumentNullException.ThrowIfNull(hotel);

        return hotel;
    }

    public async Task<Hotel> AddHotel(CreateHotelRequest createHotelRequest)
    {
        var hotel = new Hotel(createHotelRequest.Name);
        await hotelRepository.AddHotel(hotel);
        return hotel;
    }

    public async Task<Hotel> UpdateHotel(HotelId id, UpdateHotelRequest updateHotelRequest)
    {
        var existingHotel = await hotelRepository.GetHotelById(id);
        if (existingHotel is null)
        {
            throw new Exception();
        }

        if (updateHotelRequest.Name is not null)
        {
            existingHotel.UpdateHotelName(updateHotelRequest.Name);
            await hotelRepository.UpdateHotel(existingHotel);
        }

        return existingHotel;
    }

    public async Task DeleteHotel(HotelId id)
    {
        var existingHotel = await hotelRepository.GetHotelById(id);
        if (existingHotel is null)
        {
            throw new Exception();
        }

        await hotelRepository.DeleteHotel(existingHotel);
    }
}