using System.Text.Json;
using Shop.Models;

namespace Shop.Services;

public class ShopBackendApiService : IShopBackendApiService
{
    private static string _baseUrl = "http://shop-backend-api:80/";
    private readonly HttpClient _httpClient;

    public ShopBackendApiService(HttpClient httpClient) 
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_baseUrl);
    }

    public async Task<ProductViewModel?> CreateProduct(ProductCreate product)
    {

        var content = new StringContent(JsonSerializer.Serialize(product),
            System.Text.Encoding.UTF8, "application/json");

        var responseTask = await _httpClient.PostAsync("product", content);
        responseTask.EnsureSuccessStatusCode();

        if (responseTask.IsSuccessStatusCode)
        {
            var productResponse = await responseTask.Content.ReadFromJsonAsync<ProductViewModel>();
            return productResponse;
        }

        return null;
    }

    public Task DeleteProduct(ulong id)
    {
        throw new NotImplementedException();
    }

    public Task<ProductViewModel?> GetProduct(ulong id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ProductViewModel>> GetProducts()
    {
        List<ProductViewModel> products = new List<ProductViewModel>();
        var responseTask = await _httpClient.GetAsync("product");
        responseTask.EnsureSuccessStatusCode();

        if (responseTask.IsSuccessStatusCode)
        {
            var productResponse = await responseTask.Content.ReadFromJsonAsync<IEnumerable<ProductViewModel>>();
            products.AddRange(productResponse);
        }

        return products;
    }

    public Task UpdateProduct(ProductViewModel UpdatedProduct)
    {
        throw new NotImplementedException();
    }
}