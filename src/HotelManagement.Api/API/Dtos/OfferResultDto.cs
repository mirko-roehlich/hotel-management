using HotelManagement.Api.Business.Models;

namespace HotelManagement.Api.API.Dtos;

public record OfferResultDto(IEnumerable<RoomOfferDto> RoomOffers)
{
    public static OfferResultDto From(OfferResult offerResult) =>
        new(offerResult.RoomOffers.Select(RoomOfferDto.From));
}