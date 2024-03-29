namespace HotelManagement.Api.API.Dtos;

public record BookRoomsRequestDto(int HotelId, string FirstName, string LastName, IEnumerable<RoomRequestDto> RoomRequests);