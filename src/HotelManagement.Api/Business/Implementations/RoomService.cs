using HotelManagement.Api.Business.Models;
using HotelManagement.Api.Data.Models;
using HotelManagement.Api.Data.Repositories;

namespace HotelManagement.Api.Business.Implementations;

public class RoomService(IRoomRepository roomRepository, IHotelRepository hotelRepository) : IRoomService
{
    public async Task<IEnumerable<Room>> GetAllRooms(int hotelId) =>
        await roomRepository.GetAllRooms(hotelId);

    public async Task<Room> GetRoomById(int hotelId, int roomId)
    {
        var room = await roomRepository.GetRoomById(roomId, hotelId);
        ArgumentNullException.ThrowIfNull(room);

        return room;
    }

    public async Task<Room> AddRoom(int hotelId, CreateRoomRequest createRoomRequest)
    {
        ArgumentNullException.ThrowIfNull(createRoomRequest);

        var existingHotel = await hotelRepository.GetHotelById(hotelId);
        ArgumentNullException.ThrowIfNull(existingHotel);

        if (createRoomRequest.RoomNumber <= 0)
        {
            throw new InvalidOperationException("Room number must be greater than zero.");
        }

        var room = new Room
        {
            HotelId = hotelId,
            RoomNumber = createRoomRequest.RoomNumber,
            Category = createRoomRequest.Category,
            Capacity = createRoomRequest.Capacity,
            Price = createRoomRequest.Price,
            IsAvailable = true
        };
        await roomRepository.AddRoom(room);
        return room;
    }

    public async Task<Room> UpdateRoom(int hotelId, int roomId, UpdateRoomRequest updateRoomRequest)
    {
        ArgumentNullException.ThrowIfNull(updateRoomRequest);

        var existingRoom = await roomRepository.GetRoomById(hotelId, roomId);
        ArgumentNullException.ThrowIfNull(existingRoom);

        if (updateRoomRequest.RoomNumber is <= 0)
        {
            throw new InvalidCastException("Room number must be greater than zero.");
        }

        existingRoom.RoomNumber = updateRoomRequest.RoomNumber ?? existingRoom.RoomNumber;
        existingRoom.Category = updateRoomRequest.Category ?? existingRoom.Category;
        existingRoom.Capacity = updateRoomRequest.Capacity ?? existingRoom.Capacity;

        await roomRepository.UpdateRoom(existingRoom);
        return existingRoom;
    }

    public async Task DeleteRoom(int hotelId, int roomId)
    {
        var existingRoom = await roomRepository.GetRoomById(hotelId, roomId);
        ArgumentNullException.ThrowIfNull(existingRoom);

        await roomRepository.DeleteRoom(existingRoom);
    }

    public async Task<IEnumerable<Room>> GetAvailableRooms(int hotelId) => await roomRepository.GetAvailableRooms(hotelId);
}