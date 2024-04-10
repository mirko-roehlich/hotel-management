using HotelManagement.Api.API.Dtos;
using HotelManagement.Api.Data.Models;
using IntegrationTests.Setup;
using IntegrationTests.TestClients;

namespace IntegrationTests;

[Collection(nameof(HotelManagementTestCollection))]
public class OfferTests(IntegrationTestFactory factory) : IAsyncLifetime
{
    private HotelDto _hotel = null!;
    private readonly HotelTestClient _hotelTestClient = new(factory.Client);
    private readonly RoomTestClient _roomTestClient = new(factory.Client);
    private readonly OfferTestClient _offerTestClient = new(factory.Client);

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    public async Task RequestRoomOffer_SuccessfulRequest_ReturnsRoomOffer(int numberOfGuests)
    {
        var dto = new OfferRequestDto(numberOfGuests);
        var offer = await _offerTestClient.RequestRoomOffer(_hotel.Id, dto);

        await Verify(offer).UseParameters(numberOfGuests);
    }
    
    public async Task InitializeAsync()
    {
        _hotel = await _hotelTestClient.AddHotel(new CreateHotelRequestDto("Nice Hotel"));
        await _roomTestClient.AddRoom(_hotel.Id, new CreateRoomRequestDto(1, RoomCategory.Single, 1, 14.99m, "EUR"));
        await _roomTestClient.AddRoom(_hotel.Id, new CreateRoomRequestDto(2, RoomCategory.Double, 2, 24.99m, "USD"));
        await _roomTestClient.AddRoom(_hotel.Id, new CreateRoomRequestDto(3, RoomCategory.King, 2, 29.99m, "EUR"));
        await _roomTestClient.AddRoom(_hotel.Id, new CreateRoomRequestDto(4, RoomCategory.Deluxe, 4, 69.99m, "EUR"));
        await _roomTestClient.AddRoom(_hotel.Id, new CreateRoomRequestDto(1, RoomCategory.Suit, 6, 99.99m, "USD"));
    }

    public Task DisposeAsync() => factory.ResetDatabase();
}