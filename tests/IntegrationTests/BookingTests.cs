using HotelManagement.Api.API.Dtos;
using HotelManagement.Api.Data.Models;
using IntegrationTests.Setup;
using IntegrationTests.TestClients;

namespace IntegrationTests;

[Collection(nameof(HotelManagementTestCollection))]
public class BookingTests(IntegrationTestFactory factory) : IAsyncLifetime
{
    private readonly HotelTestClient _hotelTestClient = new(factory.Client);
    private readonly RoomTestClient _roomTestClient = new(factory.Client);
    private readonly BookingTestClient _bookingTestClient = new(factory.Client);
    private HotelDto _hotel;

    [Fact]
    public async Task BookRoom_BookingSingleRoom_ReturnsSingleRoom()
    {
        var dto = new BookRoomsRequestDto(_hotel.Id, "Nice", "One", new[] { new RoomRequestDto(1, RoomCategory.Single) });
        var booking = await _bookingTestClient.BookHotel(dto);

        await Verify(booking)
            .ScrubInlineGuids()
            .ScrubLinesWithReplace(line => line.Contains($"{DateTime.Now.Year}-") ? "year_number" : line);
    }

    [Fact]
    public async Task GetBookingById_WithId_ReturnsBooking()
    {
        var dto = new BookRoomsRequestDto(_hotel.Id, "Nice", "One", new[] { new RoomRequestDto(1, RoomCategory.Single) });
        var booking = await _bookingTestClient.BookHotel(dto);

        var bookingResult = await _bookingTestClient.GetBookingById(booking.BookingId);

        await Verify(bookingResult)
            .ScrubInlineGuids()
            .ScrubLinesWithReplace(line => line.Contains($"{DateTime.Now.Year}-") ? "year_number" : line);
    }

    public async Task InitializeAsync()
    {
        _hotel = await _hotelTestClient.AddHotel(new CreateHotelRequestDto("Nice Hotel"));
        await _roomTestClient.AddRoom(_hotel.Id, new CreateRoomRequestDto(1234, RoomCategory.Single, 1, 19.99m, "EUR"));
    }

    public Task DisposeAsync() => factory.ResetDatabase();
}