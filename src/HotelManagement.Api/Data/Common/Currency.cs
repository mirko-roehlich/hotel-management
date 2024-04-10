namespace HotelManagement.Api.Data.Common;

public record struct Currency(string Symbol)
{
    public override string ToString() => Symbol;
    public bool IsEmpty => string.IsNullOrWhiteSpace(Symbol);
    public static Currency Empty => new(string.Empty);

    public static readonly Currency EUR = new("EUR");
    public static readonly Currency USD = new("USD");
}