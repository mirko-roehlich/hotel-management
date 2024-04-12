using HotelManagement.Api.Business.Models;
using HotelManagement.Api.Data.Models;
using HotelManagement.Api.Data.Repositories;

namespace HotelManagement.Api.Business.Implementations;

public class BookingService(IRoomService roomService, IBookingRepository bookingRepository) : IBookingService
{
    public async Task<Booking> BookRooms(BookRoomsRequest bookRoomsRequest)
    {
        var availableRooms = await roomService.GetAvailableRooms(bookRoomsRequest.HotelId);

        List<Room> bookedRooms = [];
        foreach (var roomRequest in bookRoomsRequest.RoomRequests)
        {
            var AreEnoughRoomsAvailable = availableRooms.Count(r => r.Category == roomRequest.Category) >= roomRequest.NumberOfRooms;
            if (!AreEnoughRoomsAvailable)
            {
                throw new InvalidOperationException($"Not enough rooms of {roomRequest.Category} available.");
            }

            var selectedRooms = availableRooms.Where(r => r.Category == roomRequest.Category).Take(roomRequest.NumberOfRooms);
            bookedRooms.AddRange(selectedRooms);
        }

        var booking = new Booking
        {
            HotelId = bookRoomsRequest.HotelId,
            BookingNumber = GenerateBookingNumber(),
            RoomBookings = bookedRooms.Select(r =>
                {
                    r.IsAvailable = false;

                    return new RoomBooking
                    {
                        RoomNumber = r.RoomNumber,
                        Price = r.Price,
                        RoomId = r.Id
                    };
                })
                .ToList(),
        };

        await bookingRepository.Add(booking);

        return booking;
    }

    public async Task<Booking?> GetBookingById(Guid id) =>
        await bookingRepository.GetById(id);

    private static string GenerateBookingNumber()
    {
        var year = DateTime.Now.Year.ToString();
        Random generator = new();
        var randomNumber = generator.Next(100_000, 1_000_000).ToString("D6");

        return $"{year}-{randomNumber}";
    }
}