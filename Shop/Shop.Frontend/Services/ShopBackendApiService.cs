using Newtonsoft.Json;
using Shop.Models;
using LoggingCommunicationLibrary.Service;
using LoggingCommunicationLibrary.Dto;
using Shop.Frontend.Dto;

namespace Shop.Services;

public class ShopBackendApiService : IShopBackendApiService
{
    private static string _baseUrl = "http://shop-backend-api:80/";
    private readonly HttpClient _httpClient;
    private readonly IHttpClientFactory _httpClientFactory;
    private ILoggingCommunicationService _loggingService;

    public ShopBackendApiService(IHttpClientFactory httpClientFactory) 
    {
        _httpClientFactory = httpClientFactory;

        _httpClient = _httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri(_baseUrl);

        SetLoggingService(new LoggingCommunicationService());
    }

    public void SetLoggingService(ILoggingCommunicationService service) 
    {
        _loggingService = service;
        _loggingService.SetHttpClient(_httpClientFactory.CreateClient());
    }

    public async Task<ProductViewModel?> CreateProduct(ProductCreate product)
    {
        ProductViewModel? productResponse = null;
        string strinfiedProduct = JsonConvert.SerializeObject(product);
        StringContent content = CreateStringContent(strinfiedProduct);

        HttpResponseMessage responseTask = await _httpClient.PostAsync("product", content);

        if (responseTask.IsSuccessStatusCode)
        {
            await CreateTracingLog($"Product create Successfully: {strinfiedProduct}");
            productResponse = await SerializeSuccessfullProductCreate(productResponse, responseTask);
        }

        return productResponse;
    }

    private static async Task<ProductViewModel?> SerializeSuccessfullProductCreate(
        ProductViewModel? productResponse,
        HttpResponseMessage responseTask)
    {
        var responseJson = await responseTask.Content.ReadAsStringAsync();
        productResponse = JsonConvert.DeserializeObject<ProductViewModel>(responseJson);

        return productResponse;
    }

    public async Task<bool> DeleteProduct(ulong id) {
        HttpResponseMessage responseTask = await _httpClient.DeleteAsync($"product/{id}");

        if (responseTask.IsSuccessStatusCode)
            await CreateTracingLog($"Product id: {id}, deleted");
        
        return responseTask.IsSuccessStatusCode;
    }

    public async Task<ProductViewModel?> GetProduct(ulong id)
    {
        ProductViewModel? product = null;
        HttpResponseMessage responseTask = await _httpClient.GetAsync($"product/{id}");

        if (responseTask.IsSuccessStatusCode) 
        {
            var responseJson = await responseTask.Content.ReadAsStringAsync();
            product = JsonConvert.DeserializeObject<ProductViewModel>(responseJson);
        }

        return product;
    }

    public async Task<IEnumerable<ProductViewModel>> GetProducts()
    {
        List<ProductViewModel> products = new List<ProductViewModel>();
        HttpResponseMessage responseTask = await _httpClient.GetAsync("products");

        if (responseTask.IsSuccessStatusCode)
        {
            var responseJson = await responseTask.Content.ReadAsStringAsync();
            var productResponse = 
                JsonConvert.DeserializeObject<IEnumerable<ProductViewModel>>(responseJson) 
                ?? new List<ProductViewModel>();
            
            products.AddRange(productResponse);
        }

        return products;
    }

    public async Task<bool> UpdateProduct(ProductViewModel updatedProduct)
    {
        var content = CreateStringContent(
            JsonConvert.SerializeObject(updatedProduct));

        HttpResponseMessage responseTask = await _httpClient.PutAsync("product", content);

        if(responseTask.IsSuccessStatusCode)
            await CreateTracingLog($"Product updated Successfully: {updatedProduct}");        

        return responseTask.IsSuccessStatusCode;
    }
    
    private static StringContent CreateStringContent(string strinfiedContent) => 
        new StringContent(strinfiedContent,
            System.Text.Encoding.UTF8, "application/json");

    private async Task CreateTracingLog(string message)
    {
        var logRequest = new LogRequest(
                        System.AppDomain.CurrentDomain.FriendlyName.ToString(),
                        $"{message}",
                        DateTime.Now);

        await _loggingService.LogTracing(logRequest);
    }
}