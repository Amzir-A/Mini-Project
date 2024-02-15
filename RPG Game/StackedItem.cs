public class StackedItem
{
    public Item? Details { get; set; }
    public int Quantity { get; set; }

    public StackedItem(Item? details, int quantity)
    {
        Details = details;
        Quantity = quantity;
    }
}