namespace Shop.Dto;

public record struct ProductCreate
{
    public int Price { get; set; }
    public string Name { get; set; }
}