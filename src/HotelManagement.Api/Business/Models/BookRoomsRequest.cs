using HotelManagement.Api.Data.Models;

namespace HotelManagement.Api.Business.Models;

public record BookRoomsRequest(HotelId HotelId, string FirstName, string LastName, IEnumerable<RoomRequest> RoomRequests);