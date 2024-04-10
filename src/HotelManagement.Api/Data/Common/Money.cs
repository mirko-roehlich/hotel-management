namespace HotelManagement.Api.Data.Common;

public record Money
{
    public decimal Amount { get; }
    public Currency Currency { get; }
    public static Money Zero => new(0, Currency.Empty);

    public Money(decimal amount, Currency currency)
    {
        if (amount < 0) throw new InvalidOperationException("Money amount cannot be negative");
        Amount = Math.Round(amount, 2);
        Currency = currency;
    }
}