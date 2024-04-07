using System.Net.Http.Json;
using HotelManagement.Api.API.Dtos;
using IntegrationTests.Setup.Extensions;

namespace IntegrationTests.TestClients;

public class BookingTestClient(HttpClient httpClient)
{
    public async Task<BookingResultDto> BookHotel(BookRoomsRequestDto dto) =>
        await httpClient.PostAsJsonAsync("api/booking", dto)
            .ParseAndValidate<BookingResultDto>();

    public async Task<BookingResultDto> GetBookingById(Guid id) =>
        await httpClient.GetAsync($"/api/booking/{id}")
            .ParseAndValidate<BookingResultDto>();
}