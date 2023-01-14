using Shop.Backend.Api.Dto;

namespace Shop.Backend.Api.Models;

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

    private ulong? _id;
    public ulong? Id 
    { 
        get { return _id; }
        set 
        {
            if(_id == null)
                _id = value;
            else
                throw new InvalidOperationException(
                    "Id already set, cannot be changed");
        } 
    }
    public int Price { get; set; }
    public string Name { get; set; }

    public void UpdateProductValues(ProductUpdate product) {
        Price = product.Price;
        Name = product.Name;
    }
}