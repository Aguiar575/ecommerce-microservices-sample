namespace Shop.Models;

public class ProductModel {
    public ProductModel() { }
    public ProductModel(ulong id,
        int price,
        string name)
    {
        Id = id;
        Price = price;
        Name = name;
    }

    public ulong Id { get; set; }
    public int Price { get; set; }
    public string Name { get; set; }
}