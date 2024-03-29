using System.Text.Json.Serialization;

namespace HotelManagement.Api.Data.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RoomCategory
{
    Single = 1,
    Double = 2,
    King = 3,
    Deluxe = 4,
    Suit = 5,
}