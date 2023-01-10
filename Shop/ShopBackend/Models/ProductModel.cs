using System.ComponentModel.DataAnnotations.Schema;

namespace ShopBackend.Models;

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

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ulong Id { get; set; }
    public int Price { get; set; }
    public string Name { get; set; }

    public void UpdateProductValues(ProductModel product) {
        Price = product.Price;
        Name = product.Name;
    }
}