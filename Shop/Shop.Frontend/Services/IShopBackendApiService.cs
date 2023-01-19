using Shop.Dto;
using Shop.Models;

namespace Shop.Services;

public interface IShopBackendApiService
{
    Task<ProductViewModel?> CreateProduct(ProductCreate product);
    Task DeleteProduct(ulong id);
    Task UpdateProduct(ProductViewModel UpdatedProduct);
    Task<ProductViewModel?> GetProduct(ulong id);
    Task<IEnumerable<ProductViewModel>> GetProducts();
}