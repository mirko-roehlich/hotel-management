using HotelManagement.Api.Data.Models;

namespace HotelManagement.Api.API.Dtos;

public record BookingResultDto(string BookingId, string BookingNumber, int HotelId, IEnumerable<BookedRoomDto> BookedRooms, decimal TotalAmount)
{
    public static BookingResultDto From(Booking booking) =>
        new(booking.Id.ToString(), booking.BookingNumber, booking.HotelId.Value, booking.RoomBookings.Select(BookedRoomDto.From), booking.TotalAmount);
}