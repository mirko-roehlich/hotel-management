namespace HotelManagement.Api.Data.Common;

public record Money
{
    public decimal Amount { get; }
    public Currency Currency { get; }
    public static Money Zero => new(0, Currency.Empty);
    public bool IsZero => Amount == 0 && Currency == Currency.Empty;

    public Money(decimal amount, Currency currency)
    {
        if (amount < 0) throw new InvalidOperationException("Money amount cannot be negative");
        Amount = Math.Round(amount, 2);
        Currency = currency;
    }

    public Money Add(Money other)
    {
        if (this.IsZero) return other;
        if (other.IsZero) return this;
        if (this.Currency != other.Currency) throw new InvalidOperationException("Cannot add money of different currencies");

        return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        if (other.IsZero) return this;
        if (this.Currency != other.Currency) throw new InvalidCastException("Cannot subtract money of different currencies.");
        if (this.Amount < other.Amount) throw new InvalidOperationException("Cannot subtract more money than available.");

        return new Money(this.Amount - other.Amount, this.Currency);
    }

    public Money Scale(decimal factor)
    {
        if (factor < 0) throw new InvalidOperationException("Cannot multiply by a negative factor");

        return new Money(this.Amount * factor, this.Currency);
    }

    public static Money operator +(Money left, Money right) => left.Add(right);
    public static Money operator -(Money left, Money right) => left.Subtract(right);
    public static Money operator *(Money left, decimal right) => left.Scale(right);
    public static Money operator *(decimal left, Money right) => right.Scale(left);
}
