using HotelManagement.Api.Data.Models;

namespace HotelManagement.Api.API.Dtos;

public record HotelDto(int Id, string Name)
{
    public static HotelDto From(Hotel hotel) =>
        new(hotel.Id, hotel.Name);
}

public record CreateHotelRequestDto(string Name);

public record UpdateHotelRequestDto(string Name);