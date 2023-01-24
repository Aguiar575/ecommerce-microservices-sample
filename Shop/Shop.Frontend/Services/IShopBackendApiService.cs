using LoggingCommunicationLibrary.Service;
using Shop.Frontend.Dto;
using Shop.Models;

namespace Shop.Services;

public interface IShopBackendApiService
{
    void SetLoggingService(ILoggingCommunicationService service);
    Task<ProductViewModel?> CreateProduct(ProductCreate product);
    Task<bool> DeleteProduct(ulong id);
    Task<bool> UpdateProduct(ProductViewModel updatedProduct);
    Task<ProductViewModel?> GetProduct(ulong id);
    Task<IEnumerable<ProductViewModel>> GetProducts();
}