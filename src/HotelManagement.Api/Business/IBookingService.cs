using HotelManagement.Api.Business.Models;
using HotelManagement.Api.Data.Models;

namespace HotelManagement.Api.Business;

public interface IBookingService
{
    Task<Booking> BookRooms(BookRoomsRequest bookRoomsRequest);
    Task<Booking?> GetBookingById(Guid id);
}