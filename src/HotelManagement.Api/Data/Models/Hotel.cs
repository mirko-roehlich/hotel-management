namespace HotelManagement.Api.Data.Models;

public class Hotel
{
    public HotelId Id { get; private set; }
    public string Name { get; private set; }
    private readonly List<Room> _rooms;
    public IReadOnlyCollection<Room> Rooms => _rooms.AsReadOnly();

    public Hotel(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Id = HotelId.Empty;
        Name = name;
        _rooms = [];
    }

    public void UpdateHotelName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Name = name;
    }
}

public readonly record struct HotelId(int Value)
{
    public static HotelId Empty => new(0);
    public static implicit operator int(HotelId hotelId) => hotelId.Value;
    public override string ToString() => Value.ToString();
}