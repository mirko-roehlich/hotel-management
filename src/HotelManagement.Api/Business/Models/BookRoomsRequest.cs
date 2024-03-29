namespace HotelManagement.Api.Business.Models;

public record BookRoomsRequest(int HotelId, string FirstName, string LastName, IEnumerable<RoomRequest> RoomRequests);