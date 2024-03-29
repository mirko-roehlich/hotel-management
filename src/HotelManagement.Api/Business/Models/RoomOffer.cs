using HotelManagement.Api.Data.Models;

namespace HotelManagement.Api.Business.Models;

public record RoomOffer(RoomCategory Category, string Description, decimal Price);