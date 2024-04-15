using HotelManagement.Api.Data.Common;

namespace HotelManagement.Api.Data.Models;

public class Hotel
{
    public HotelId Id { get; private set; }
    public string Name { get; private set; }
    private readonly List<Room> _rooms;
    public IReadOnlyCollection<Room> Rooms => _rooms.AsReadOnly();
    public IReadOnlyCollection<Room> AvailableRooms => _rooms.Where(r => r.IsAvailable).ToList();

    private Hotel() {}
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

    private static int GetMaxFloors(HotelId hotelId) =>
        hotelId.Value switch
        {
            1 => 12,
            2 => 11,
            3 => 14,
            _ => 10
        };

    public Room AddNewRoom(int roomNumber, RoomCategory category, int capacity, Money price)
    {
        var expectedRoomNumber = ExpectedRoomNumber(roomNumber);

        var room = new Room(Id, expectedRoomNumber, category, capacity, price);
        _rooms.Add(room);

        return room;
    }

    private RoomNumber ExpectedRoomNumber(int roomNumber)
    {
        var expectedRoomNumber = new RoomNumber(roomNumber);

        if (expectedRoomNumber.Floor > GetMaxFloors(Id))
        {
            throw new InvalidOperationException($"Room number is not valid. Hotel doesn't have floors higher than {GetMaxFloors(Id)}");
        }

        return expectedRoomNumber;
    }

    public void RemoveRoom(RoomId roomId)
    {
        var room = _rooms.Single(r => r.Id == roomId);
        _rooms.Remove(room);
    }

    public void UpdateRoom(RoomId roomId, int? roomNumber, RoomCategory? category, int? capacity, decimal? price, string? currency)
    {
        var room = _rooms.Single(r => r.Id == roomId);

        if (roomNumber.HasValue)
        {
            var newRoomNumber = ExpectedRoomNumber(roomNumber.Value);
            room.ChangeRoomNumber(newRoomNumber);
        }

        if (category.HasValue)
        {
            room.ChangeCategory(category.Value);
        }

        if (capacity.HasValue)
        {
            room.ResizeCapacity(capacity.Value);
        }

        var newCurrency = currency switch
        {
            null => room.Price.Currency,
            _ => new Currency(currency)
        };

        var amount = price switch
        {
            null => room.Price.Amount,
            _ => price.Value
        };

        room.AdjustPrice(new Money(amount, newCurrency));
    }

    public IReadOnlyCollection<Room> BookRooms(IDictionary<RoomCategory, int> categoryRequests)
    {
        List<Room> bookedRooms = [];
        foreach (var categoryRequest in categoryRequests)
        {
            var enoughRoomsAvailable = AvailableRooms.Select(r => r.Category == categoryRequest.Key).Count() >= categoryRequest.Value;
            if (!enoughRoomsAvailable)
            {
                throw new InvalidOperationException($"Not enough rooms of {categoryRequest.Key} available.");
            }

            var selectedRooms = AvailableRooms.Where(r => r.Category == categoryRequest.Key).Take(categoryRequest.Value);
            bookedRooms.AddRange(selectedRooms);
        }

        foreach (var room in bookedRooms)
        {
            room.Book();
        }

        return bookedRooms.AsReadOnly();
    }
}

public readonly record struct HotelId(int Value)
{
    public static HotelId Empty => new(0);
    public static implicit operator int(HotelId hotelId) => hotelId.Value;
    public override string ToString() => Value.ToString();
}