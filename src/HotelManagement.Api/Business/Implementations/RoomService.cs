using HotelManagement.Api.Business.Models;
using HotelManagement.Api.Data.Models;
using HotelManagement.Api.Data.Repositories;

namespace HotelManagement.Api.Business.Implementations;

public class RoomService(IHotelRepository hotelRepository) : IRoomService
{
    public async Task<IEnumerable<Room>> GetAllRooms(HotelId hotelId)
    {
        var hotel = await hotelRepository.GetHotelById(hotelId);
        ArgumentNullException.ThrowIfNull(hotel);
        
        return hotel.Rooms;
    }

    public async Task<Room> GetRoomById(HotelId hotelId, RoomId roomId)
    {
        var hotel = await hotelRepository.GetHotelById(hotelId);
        ArgumentNullException.ThrowIfNull(hotel);
        
        return hotel.Rooms.SingleOrDefault(r => r.Id == roomId) ?? throw new ArgumentNullException();
    }

    public async Task<Room> AddRoom(HotelId hotelId, CreateRoomRequest createRoomRequest)
    {
        ArgumentNullException.ThrowIfNull(createRoomRequest);

        var existingHotel = await hotelRepository.GetHotelById(hotelId);
        ArgumentNullException.ThrowIfNull(existingHotel);
        
        var room = existingHotel.AddNewRoom(createRoomRequest.RoomNumber, createRoomRequest.Category, createRoomRequest.Capacity, createRoomRequest.Price);
        await hotelRepository.Save();
        
        return room;
    }

    public async Task<Room> UpdateRoom(HotelId hotelId, RoomId roomId, UpdateRoomRequest updateRoomRequest)
    {
        ArgumentNullException.ThrowIfNull(updateRoomRequest);

        var hotel = await hotelRepository.GetHotelById(hotelId);
        ArgumentNullException.ThrowIfNull(hotel);

        hotel.UpdateRoom(roomId, updateRoomRequest.RoomNumber, updateRoomRequest.Category, updateRoomRequest.Capacity, updateRoomRequest.Price, updateRoomRequest.Currency);
        await hotelRepository.Save();

        return hotel.Rooms.Single(r => r.Id == roomId);
    }

    public async Task DeleteRoom(HotelId hotelId, RoomId roomId)
    {
        var hotel = await hotelRepository.GetHotelById(hotelId);
        ArgumentNullException.ThrowIfNull(hotel);

        hotel.RemoveRoom(roomId);
        await hotelRepository.Save();
    }
}