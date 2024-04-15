using HotelManagement.Api.Business.Models;
using HotelManagement.Api.Data.Models;
using HotelManagement.Api.Data.Repositories;

namespace HotelManagement.Api.Business.Implementations;

public class OfferService(IHotelRepository hotelRepository) : IOfferService
{
    public async Task<OfferResult> GetOffer(OfferRequest offerRequest, HotelId hotelId)
    {
        var hotel = await hotelRepository.GetHotelById(hotelId);
        ArgumentNullException.ThrowIfNull(hotel);

        var categoriesWithCapacity = hotel.AvailableRooms
            .GroupBy(r => r.Category)
            .Where(g => g.Sum(v => v.Capacity) >= offerRequest.NumberOfGuests)
            .Select(g => g.First());

        var roomOffers = categoriesWithCapacity.Select(r => new RoomOffer(r.Category, "", r.Price.Amount, r.Price.Currency.Symbol));

        return new OfferResult(roomOffers);
    }
}