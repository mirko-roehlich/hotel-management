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

        var room = new Room
        {
            HotelId = hotelId,
            RoomNumber = RoomNumber.Create(createRoomRequest.RoomNumber, GetMaxFloors(hotelId)),
            Category = createRoomRequest.Category,
            Capacity = createRoomRequest.Capacity,
            Price = createRoomRequest.Price,
            IsAvailable = true
        };
        await roomRepository.AddRoom(room);
        return room;
    }

    private static int GetMaxFloors(int hotelId) =>
        hotelId switch
        {
            1 => 12,
            2 => 11,
            3 => 14,
            _ => 10
        };

    public async Task<Room> UpdateRoom(int hotelId, int roomId, UpdateRoomRequest updateRoomRequest)
    {
        ArgumentNullException.ThrowIfNull(updateRoomRequest);

        var existingRoom = await roomRepository.GetRoomById(hotelId, roomId);
        ArgumentNullException.ThrowIfNull(existingRoom);

        if (updateRoomRequest.RoomNumber.HasValue)
        {
            existingRoom.RoomNumber = RoomNumber.Create(updateRoomRequest.RoomNumber.Value, GetMaxFloors(hotelId));
        }

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