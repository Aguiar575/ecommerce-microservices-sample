using Shop.Dto;
using Shop.Models;

namespace Shop.Services;

public interface IShopBackendApiService
{
    Task<ProductViewModel?> CreateProduct(ProductCreate product);
    Task<bool> DeleteProduct(ulong id);
    Task<bool> UpdateProduct(ProductViewModel updatedProduct);
    Task<ProductViewModel?> GetProduct(ulong id);
    Task<IEnumerable<ProductViewModel>> GetProducts();
}