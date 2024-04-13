using System.Net;
using FluentAssertions;
using HotelManagement.Api.API.Dtos;
using HotelManagement.Api.Data.Models;
using IntegrationTests.Setup;
using IntegrationTests.TestClients;

namespace IntegrationTests;

[Collection(nameof(HotelManagementTestCollection))]
public class RoomTests(IntegrationTestFactory factory) : IAsyncLifetime
{
    private HotelDto _hotel = null!;
    private RoomDto _room = null!;
    private readonly HotelTestClient _hotelTestClient = new(factory.Client);
    private readonly RoomTestClient _roomTestClient = new(factory.Client);

    [Theory]
    [InlineData(1234, RoomCategory.Single, 1, 14.99)]
    [InlineData(5678,RoomCategory.Double, 2, 29.99)]
    [InlineData(90123, RoomCategory.King, 2, 39.99)]
    [InlineData(45679, RoomCategory.Deluxe, 4, 49.99)]
    [InlineData(123456, RoomCategory.Suit, 6, 99.99)]
    public async Task AddRoom_ToExistingHotel_ReturnsRoom(int roomNumber, RoomCategory category, int capacity, decimal price)
    {
        var dto = new CreateRoomRequestDto(roomNumber, category, capacity, price);
        var room = await _roomTestClient.AddRoom(_hotel.Id, dto);

        await Verify(room).UseParameters(category, capacity, price);
    }
    
    [Fact]
    public async Task GetRoom_FromCreatedHotel_ReturnsRoom()
    {
        var room = await _roomTestClient.GetRoom(_hotel.Id, _room.Id);

        await Verify(room);
    }

    [Fact]
    public async Task GetRoom_FromNonExistingHotel_ReturnsNotFound()
    {
        var responseMessage = await _roomTestClient.GetRoomRaw(0, _room.Id);

        responseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateRoom_UpdateExistingRoom_ReturnsUpdatedRoom()
    {
        var dto = new UpdateRoomRequestDto(1234, RoomCategory.Suit, 4, 49.99m);
        var room = await _roomTestClient.UpdateRoom(_hotel.Id, _room.Id, dto);

        await Verify(room);
    }

    [Fact]
    public async Task DeleteRoom_RemoveExistingRoom_HotelDoesntContainRoom()
    {
        await _roomTestClient.DeleteRoom(_hotel.Id, _room.Id);

        var responseMessage = await _roomTestClient.GetRoomRaw(_hotel.Id, _room.Id);

        responseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    public async Task InitializeAsync()
    {
        _hotel = await _hotelTestClient.AddHotel(new CreateHotelRequestDto("Nice Hotel"));
        _room = await _roomTestClient.AddRoom(_hotel.Id, new CreateRoomRequestDto(7032, RoomCategory.Double, 2, 29.99m));
    }

    public Task DisposeAsync() => factory.ResetDatabase();
}