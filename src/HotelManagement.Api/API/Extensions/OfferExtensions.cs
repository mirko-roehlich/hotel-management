using HotelManagement.Api.API.Dtos;
using HotelManagement.Api.Business.Models;

namespace HotelManagement.Api.API.Extensions;

public static class OfferExtensions
{
    public static OfferRequest ToDomain(this OfferRequestDto dto) =>
        new(dto.NumberOfGuests);
}