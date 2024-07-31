namespace Domain.Accounts.ValueObjects;

public sealed record ReciptItem
{
    public string ItemName { get; private set; }
    public string ItemDescription { get; private set; }
    public string Category { get; private set; }
    public float Quantity { get; private set; }
    public decimal Price { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal TotalPrice { get; private set; }

    public ReciptItem(string itemName, string itemDescription, string category, float quantity, decimal price, decimal unitPrice, decimal totalPrice)
    {
        ItemName = itemName;
        ItemDescription = itemDescription;
        Category = category;
        Quantity = quantity;
        Price = price;
        UnitPrice = unitPrice;
        TotalPrice = totalPrice;
    }

}
