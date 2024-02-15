public class Item
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }

    public Item(int id, string name, int price)
    {
        ID = id;
        Name = name;
        Price = price;
    }
}