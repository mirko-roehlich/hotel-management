using HotelManagement.Api.Business.Models;

namespace HotelManagement.Api.API.Dtos;

public record RoomOfferDto(string Category, string Description, decimal Price)
{
    public static RoomOfferDto From(RoomOffer roomOffer) =>
        new(roomOffer.Category.ToString(), roomOffer.Description, roomOffer.Price);
}