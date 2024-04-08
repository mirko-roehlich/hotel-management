using HotelManagement.Api.Business.Models;
using HotelManagement.Api.Data.Models;

namespace HotelManagement.Api.Business;

public interface IOfferService
{
    Task<OfferResult> GetOffer(OfferRequest offerRequest, HotelId hotelId);
}