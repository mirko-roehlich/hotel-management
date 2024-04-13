using HotelManagement.Api.Data.Models;

namespace HotelManagement.Api.API.Dtos;

public record BookingResultDto(string BookingId, string BookingNumber, int HotelId, IEnumerable<BookedRoomDto> BookedRooms, decimal TotalAmount, string Currency)
{
    public static BookingResultDto From(Booking booking) =>
        new(booking.Id.ToString(), booking.BookingNumber, booking.HotelId, booking.RoomBookings.Select(BookedRoomDto.From), booking.TotalAmount.Amount, booking.TotalAmount.Currency.Symbol);
}