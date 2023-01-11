using Shop.Models;

namespace Shop.Services;

public interface IShopBackendApiService
{
    Task<ProductViewModel?> CreateProduct(ProductViewModel product);
    Task DeleteProduct(ulong id);
    Task UpdateProduct(ProductViewModel UpdatedProduct);
    Task<ProductViewModel?> GetProduct(ulong id);
    Task<IEnumerable<ProductViewModel>> GetProducts();
}