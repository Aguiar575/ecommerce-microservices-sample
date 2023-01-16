using Shop.Backend.Api.Dto;

namespace Shop.Backend.Api.Models;

public class ProductModel {
    internal ProductModel() { }
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

    public ProductModel? UpdateProductValues(ProductUpdate product) =>
        this.Id is not null ? 
        new ProductModel(this.Id.Value, product.Price, product.Name) :
        null;
}