using System.Net;
using Moq.Protected;
using Newtonsoft.Json;
using Shop.Dto;
using Shop.Models;
using Shop.Services;

namespace LoggingCommunicationLibrary.Tests;

public class ShopBackendApiServiceTests
{
    [Fact]
    public async Task CreateProduct_Should_Return_ProductViewModel()
    {
        var responseBody = new ProductViewModel { 
            Id = 999999, 
            Price = 10, 
            Name = "Test product" 
        };
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(responseBody))
        };

        Mock<HttpMessageHandler> mockMessageHandler =
            MockMessageHandler(response);
        var sut = new ShopBackendApiService(
            new HttpClient(mockMessageHandler.Object));

        var productCreate = new ProductCreate { Price = 10, Name = "Test product" };
        ProductViewModel? responseTask = await sut.CreateProduct(productCreate);

        Assert.NotNull(responseTask);
        Assert.True(responseTask?.Equals(responseBody));
    }

    private static Mock<HttpMessageHandler> MockMessageHandler(
        HttpResponseMessage response)
    {
        var mockMessageHandler = new Mock<HttpMessageHandler>();
        mockMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        return mockMessageHandler;
    }
}