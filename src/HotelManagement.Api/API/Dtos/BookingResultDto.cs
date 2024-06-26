using HotelManagement.Api.Data.Models;

namespace HotelManagement.Api.API.Dtos;

public record BookingResultDto(Guid BookingId, string BookingNumber, int HotelId, IEnumerable<BookedRoomDto> BookedRooms, decimal TotalAmount)
{
    public static BookingResultDto From(Booking booking) =>
        new(booking.Id, booking.BookingNumber, booking.HotelId, booking.RoomBookings.Select(BookedRoomDto.From), booking.TotalAmount);
}