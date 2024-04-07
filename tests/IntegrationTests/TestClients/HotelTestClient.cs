using System.Net;
using System.Net.Http.Json;
using HotelManagement.Api.API.Dtos;
using IntegrationTests.Setup.Extensions;

namespace IntegrationTests.TestClients;

public class HotelTestClient(HttpClient httpClient)
{
    public async Task<HotelDto> AddHotel(CreateHotelRequestDto dto) =>
        await httpClient.PostAsJsonAsync("/api/hotel", dto)
            .ParseAndValidate<HotelDto>(HttpStatusCode.Created);

    public async Task<HotelDto> GetHotel(int hotelId) =>
        await GetHotelRaw(hotelId)
            .ParseAndValidate<HotelDto>();

    public async Task<HttpResponseMessage> GetHotelRaw(int hotelId) =>
        await httpClient.GetAsync($"/api/hotel/{hotelId}");

    public async Task<HotelDto> UpdateHotel(int hotelId, UpdateHotelRequestDto updateDto) =>
        await httpClient.PutAsJsonAsync($"/api/hotel/{hotelId}", updateDto)
            .ParseAndValidate<HotelDto>();

    public async Task DeleteHotel(int hotelId) =>
        await httpClient.DeleteAsync($"/api/hotel/{hotelId}");
}