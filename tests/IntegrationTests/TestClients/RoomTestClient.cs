using System.Net;
using System.Net.Http.Json;
using HotelManagement.Api.API.Dtos;
using IntegrationTests.Setup.Extensions;

namespace IntegrationTests.TestClients;

public class RoomTestClient(HttpClient httpClient)
{
    public async Task<RoomDto> AddRoom(int hotelId, CreateRoomRequestDto dto) =>
        await httpClient.PostAsJsonAsync($"/api/hotel/{hotelId}/room", dto)
            .ParseAndValidate<RoomDto>(HttpStatusCode.Created);

    public async Task<RoomDto> GetRoom(int hotelId, int roomId) =>
        await GetRoomRaw(hotelId, roomId)
            .ParseAndValidate<RoomDto>();
    public async Task<HttpResponseMessage> GetRoomRaw(int hotelId, int roomId) =>
        await httpClient.GetAsync($"api/hotel/{hotelId}/Room/{roomId}");

    public async Task<RoomDto> UpdateRoom(int hotelId, int roomId, UpdateRoomRequestDto dto) =>
        await httpClient.PutAsJsonAsync($"api/hotel/{hotelId}/room/{roomId}", dto)
            .ParseAndValidate<RoomDto>();

    public async Task DeleteRoom(int hotelId, int roomId) =>
        await httpClient.DeleteAsync($"api/hotel/{hotelId}/Room/{roomId}");
}