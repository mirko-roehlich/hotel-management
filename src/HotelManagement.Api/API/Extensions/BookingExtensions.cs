using HotelManagement.Api.API.Dtos;
using HotelManagement.Api.Business.Models;
using HotelManagement.Api.Data.Models;

namespace HotelManagement.Api.API.Extensions;

public static class BookingExtensions
{
    public static RoomRequest ToDomain(this RoomRequestDto dto) =>
        new(dto.NumberOfRooms, dto.Category);

    public static BookRoomsRequest ToDomain(this BookRoomsRequestDto dto) =>
        new(new HotelId(dto.HotelId), dto.FirstName, dto.LastName, dto.RoomRequests.Select(r => r.ToDomain()));
}