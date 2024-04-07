using System.Net.Http.Json;
using HotelManagement.Api.API.Dtos;
using IntegrationTests.Setup.Extensions;

namespace IntegrationTests.TestClients;

public class OfferTestClient(HttpClient httpClient)
{
    public async Task<OfferResultDto> RequestRoomOffer(int hotelId, OfferRequestDto dto) =>
        await httpClient.PostAsJsonAsync($"/api/hotel/{hotelId}/offer", dto)
            .ParseAndValidate<OfferResultDto>();
}