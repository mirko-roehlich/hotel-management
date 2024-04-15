using HotelManagement.Api.Business.Models;
using HotelManagement.Api.Data.Models;
using HotelManagement.Api.Data.Repositories;

namespace HotelManagement.Api.Business.Implementations;

public class BookingService(IHotelRepository hotelRepository, IBookingRepository bookingRepository) : IBookingService
{
    public async Task<Booking> BookRooms(BookRoomsRequest bookRoomsRequest)
    {
        var hotel = await hotelRepository.GetHotelById(bookRoomsRequest.HotelId);
        var categoryRequests = bookRoomsRequest.RoomRequests.ToDictionary(roomRequest => roomRequest.Category, roomRequest => roomRequest.NumberOfRooms);
        var selectedRooms = hotel.BookRooms(categoryRequests);
        var roomBookings = selectedRooms.Select(r => new RoomBooking
            {
                RoomNumber = r.RoomNumber,
                Price = r.Price,
                RoomId = r.Id
            })
            .ToList();
        
        var booking = new Booking
        {
            Id = BookingId.Create(),
            HotelId = bookRoomsRequest.HotelId,
            BookingNumber = GenerateBookingNumber(),
            RoomBookings = roomBookings,
        };

        await bookingRepository.Add(booking);
        return booking;
    }

    public async Task<Booking?> GetBookingById(BookingId id) =>
        await bookingRepository.GetById(id);

    private static string GenerateBookingNumber()
    {
        var year = DateTime.Now.Year.ToString();
        Random generator = new();
        var randomNumber = generator.Next(100_000, 1_000_000).ToString("D6");

        return $"{year}-{randomNumber}";
    }
}