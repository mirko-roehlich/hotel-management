using System.Net;
using FluentAssertions;
using HotelManagement.Api.API.Dtos;
using IntegrationTests.Setup;
using IntegrationTests.TestClients;

namespace IntegrationTests;

[Collection(nameof(HotelManagementTestCollection))]
public class HotelTests(IntegrationTestFactory factory) : IAsyncLifetime
{
    private HotelDto _createdHotel = null!;
    private readonly HotelTestClient _hotelTestClient = new(factory.Client);

    [Fact]
    public async Task AddHotel_NonExistingHotel_ReturnsCreatedHotel()
    {
        CreateHotelRequestDto dto = new("Nice Hotel");
        var hotel = await _hotelTestClient.AddHotel(dto);

        await Verify(hotel);
    }

    [Fact]
    public async Task GetHotel_WhenHotelCreated_ReturnsHotel()
    {
        var hotel = await _hotelTestClient.GetHotel(_createdHotel.Id);
        await Verify(hotel);
    }

    [Fact]
    public async Task GetHotel_WithNonExistingId_ThrowsException()
    {
        var responseMessage = await _hotelTestClient.GetHotelRaw(0);
        responseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task UpdateHotel_HotelWithNewName_ReturnHotelWithNewName()
    {
        var updateDto = new UpdateHotelRequestDto("New Name");
        var hotel = _hotelTestClient.UpdateHotel(_createdHotel.Id, updateDto);

        await Verify(hotel);
    }

    [Fact]
    public async Task DeleteHotel_ExistingHotel_ShouldDeleteHotel()
    {
        await _hotelTestClient.DeleteHotel(_createdHotel.Id);

        var responseMessage = await _hotelTestClient.GetHotelRaw(_createdHotel.Id);
        responseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    public async Task InitializeAsync() => _createdHotel = await _hotelTestClient.AddHotel(new CreateHotelRequestDto("Nice Hotel"));

    public Task DisposeAsync() => factory.ResetDatabase();
}