using Newtonsoft.Json;
using Shop.Dto;
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
        ProductViewModel? productResponse = null;
        var content = new StringContent(JsonConvert.SerializeObject(product),
            System.Text.Encoding.UTF8, "application/json");

        var responseTask = await _httpClient.PostAsync("product", content);

        if (responseTask.IsSuccessStatusCode)
        {
            var responseJson = await responseTask.Content.ReadAsStringAsync();
            productResponse = JsonConvert.DeserializeObject<ProductViewModel>(responseJson);
        }

        return productResponse;
    }

    public async Task DeleteProduct(ulong id) =>
        await _httpClient.DeleteAsync($"product/{id}");

    public async Task<ProductViewModel?> GetProduct(ulong id)
    {
        ProductViewModel? productResponse = null;

        var responseTask = await _httpClient.GetAsync($"product/{id}");

        if (responseTask.IsSuccessStatusCode)
            productResponse = await responseTask.Content.ReadFromJsonAsync<ProductViewModel>();

        return productResponse;
    }

    public async Task<IEnumerable<ProductViewModel>> GetProducts()
    {
        List<ProductViewModel> products = new List<ProductViewModel>();
        var responseTask = await _httpClient.GetAsync("products");

        if (responseTask.IsSuccessStatusCode)
        {
            var productResponse = 
                await responseTask.Content.ReadFromJsonAsync<IEnumerable<ProductViewModel>>()
                ?? new List<ProductViewModel>();

            products.AddRange(productResponse);
        }

        return products;
    }

    public async Task UpdateProduct(ProductViewModel UpdatedProduct)
    {
        var content = new StringContent(JsonConvert.SerializeObject(UpdatedProduct),
            System.Text.Encoding.UTF8, "application/json");

        var responseTask = await _httpClient.PutAsync("product", content);
    }
}