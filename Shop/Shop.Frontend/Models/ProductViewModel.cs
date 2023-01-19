namespace Shop.Models;

public class ProductViewModel : IEquatable<ProductViewModel>
{
    public ulong Id { get; set; }
    public int Price { get; set; }
    public string Name { get; set; }

    public bool Equals(ProductViewModel? other)
    {
        return this.Id.Equals(other?.Id)
            && this.Price.Equals(other?.Price)
            && this.Name.Equals(other?.Name);
    }
}