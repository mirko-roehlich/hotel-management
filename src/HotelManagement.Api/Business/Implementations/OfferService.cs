using HotelManagement.Api.Business.Models;
using HotelManagement.Api.Data.Repositories;

namespace HotelManagement.Api.Business.Implementations;

public class OfferService(IRoomRepository roomRepository) : IOfferService
{
    public async Task<OfferResult> GetOffer(OfferRequest offerRequest, int hotelId)
    {
        var availableRooms = await roomRepository.GetAvailableRooms(hotelId);

        var categoriesWithCapacity = availableRooms
            .GroupBy(r => r.Category)
            .Where(g => g.Sum(v => v.Capacity) >= offerRequest.NumberOfGuests)
            .Select(g => g.First());

        var roomOffers = categoriesWithCapacity.Select(r => new RoomOffer(r.Category, "", r.Price));

        return new OfferResult(roomOffers);
    }
}