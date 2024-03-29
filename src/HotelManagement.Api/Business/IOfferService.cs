using HotelManagement.Api.Business.Models;

namespace HotelManagement.Api.Business;

public interface IOfferService
{
    Task<OfferResult> GetOffer(OfferRequest offerRequest, int hotelId);
}