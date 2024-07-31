namespace Domain.Accounts.ValueObjects;

public sealed record PaymentMethod
{
    public string Last4 { get; private set; }
    public string Type { get; private set; }

    public PaymentMethod(string last4, string type)
    {
        Last4 = last4;
        Type = type;
    }
}
