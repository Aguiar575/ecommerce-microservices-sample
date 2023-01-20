using System.Net;
using LoggingCommunicationLibrary.Dto;
using LoggingCommunicationLibrary.Service;
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

        Mock<IHttpClientFactory> mockHttpClientFactory = 
            new Mock<IHttpClientFactory>();
        mockHttpClientFactory.Setup(e => e.CreateClient(string.Empty)).Returns(MockHttpClient(response));

        Mock<ILoggingCommunicationService> mockLoggingService = 
            new Mock<ILoggingCommunicationService>();

        var sut = new ShopBackendApiService(
            mockHttpClientFactory.Object, mockLoggingService.Object);

        var productCreate = new ProductCreate { Price = 10, Name = "Test product" };
        ProductViewModel? responseTask = await sut.CreateProduct(productCreate);

        Assert.NotNull(responseTask);
        Assert.True(responseTask?.Equals(responseBody));
    }

    [Fact]
    public async Task CreateProduct_Should_Log_Product_If_StatusCode_Is_Successfull()
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

        Mock<IHttpClientFactory> mockHttpClientFactory = 
            new Mock<IHttpClientFactory>();
        mockHttpClientFactory.Setup(e => e.CreateClient(string.Empty))
            .Returns(MockHttpClient(response));

        Mock<ILoggingCommunicationService> mockLoggingService = 
            new Mock<ILoggingCommunicationService>();

        var sut = new ShopBackendApiService(
            mockHttpClientFactory.Object, mockLoggingService.Object);

        var productCreate = new ProductCreate { Price = 10, Name = "Test product" };
        ProductViewModel? responseTask = await sut.CreateProduct(productCreate);

        mockLoggingService.Verify(
            e => e.LogTracing(It.IsAny<LogRequest>()), Times.Once());
    }

    [Fact]
    public async Task CreateProduct_Should_Return_Null_ProductViewModel_If_StatusCode_Is_Not_scuccessfull()
    {
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.InternalServerError
        };

        Mock<IHttpClientFactory> mockHttpClientFactory = 
            new Mock<IHttpClientFactory>();
        mockHttpClientFactory.Setup(e => e.CreateClient(string.Empty)).Returns(MockHttpClient(response));

        Mock<ILoggingCommunicationService> mockLoggingService = 
            new Mock<ILoggingCommunicationService>();

        var sut = new ShopBackendApiService(
            mockHttpClientFactory.Object, mockLoggingService.Object);

        var productCreate = new ProductCreate { Price = 10, Name = "Test product" };
        ProductViewModel? responseTask = await sut.CreateProduct(productCreate);

        Assert.Null(responseTask);
    }

    [Fact]
    public async Task DeleteProduct_Should_Return_True_If_Product_Was_Deleted()
    {
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK
        };

        Mock<IHttpClientFactory> mockHttpClientFactory = 
            new Mock<IHttpClientFactory>();
        mockHttpClientFactory.Setup(e => e.CreateClient(string.Empty))
            .Returns(MockHttpClient(response));

        Mock<ILoggingCommunicationService> mockLoggingService = 
            new Mock<ILoggingCommunicationService>();

        var sut = new ShopBackendApiService(
            mockHttpClientFactory.Object, mockLoggingService.Object);

        bool result = await sut.DeleteProduct(9999999);

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteProduct_Should_Log_If_Product_Was_Deleted()
    {
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK
        };

        Mock<IHttpClientFactory> mockHttpClientFactory = 
            new Mock<IHttpClientFactory>();
        mockHttpClientFactory.Setup(e => e.CreateClient(string.Empty))
            .Returns(MockHttpClient(response));

        Mock<ILoggingCommunicationService> mockLoggingService = 
            new Mock<ILoggingCommunicationService>();

        var sut = new ShopBackendApiService(
            mockHttpClientFactory.Object, mockLoggingService.Object);

        bool result = await sut.DeleteProduct(9999999);

        mockLoggingService.Verify(
            e => e.LogTracing(It.IsAny<LogRequest>()), Times.Once());
    }

    [Fact]
    public async Task DeleteProduct_Should_Not_Log_If_Product_Was_Not_Deleted()
    {
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.InternalServerError
        };

        Mock<IHttpClientFactory> mockHttpClientFactory = 
            new Mock<IHttpClientFactory>();
        mockHttpClientFactory.Setup(e => e.CreateClient(string.Empty))
            .Returns(MockHttpClient(response));

        Mock<ILoggingCommunicationService> mockLoggingService = 
            new Mock<ILoggingCommunicationService>();

        var sut = new ShopBackendApiService(
            mockHttpClientFactory.Object, mockLoggingService.Object);

        bool result = await sut.DeleteProduct(9999999);

        Assert.True(!result);
    }

    [Fact]
    public async Task DeleteProduct_Should__Return_False_If_Product_Was_Not_Deleted()
    {
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.InternalServerError
        };

        Mock<IHttpClientFactory> mockHttpClientFactory = 
            new Mock<IHttpClientFactory>();
        mockHttpClientFactory.Setup(e => e.CreateClient(string.Empty))
            .Returns(MockHttpClient(response));

        Mock<ILoggingCommunicationService> mockLoggingService = 
            new Mock<ILoggingCommunicationService>();

        var sut = new ShopBackendApiService(
            mockHttpClientFactory.Object, mockLoggingService.Object);

        await sut.DeleteProduct(9999999);

        mockLoggingService.Verify(
            e => e.LogTracing(It.IsAny<LogRequest>()), Times.Never());
    }

    [Fact]
    public async Task GetProduct_Should_Return_ProductViewModelCollection_If_Request_Was_Successfull()
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

        Mock<IHttpClientFactory> mockHttpClientFactory = 
            new Mock<IHttpClientFactory>();
        mockHttpClientFactory.Setup(e => e.CreateClient(string.Empty)).Returns(MockHttpClient(response));

        Mock<ILoggingCommunicationService> mockLoggingService = 
            new Mock<ILoggingCommunicationService>();

        var sut = new ShopBackendApiService(
            mockHttpClientFactory.Object, mockLoggingService.Object);

        ProductViewModel? responseTask = await sut.GetProduct(999999);

        Assert.NotNull(responseTask);
        Assert.True(responseTask?.Equals(responseBody));
    }

    [Fact]
    public async Task GetProduct_Should_Return_Null_If_Request_Was_Not_Successfull()
    {
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK
        };

        Mock<IHttpClientFactory> mockHttpClientFactory = 
            new Mock<IHttpClientFactory>();
        mockHttpClientFactory.Setup(e => e.CreateClient(string.Empty)).Returns(MockHttpClient(response));

        Mock<ILoggingCommunicationService> mockLoggingService = 
            new Mock<ILoggingCommunicationService>();

        var sut = new ShopBackendApiService(
            mockHttpClientFactory.Object, mockLoggingService.Object);

        ProductViewModel? responseTask = await sut.GetProduct(999999);

        Assert.Null(responseTask);
    }

    [Fact]
    public async Task UpdateProduct_Should_Return_True_When_Update_Was_Successfull()
    {
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK
        };

        Mock<IHttpClientFactory> mockHttpClientFactory = 
            new Mock<IHttpClientFactory>();
        mockHttpClientFactory.Setup(e => e.CreateClient(string.Empty)).Returns(MockHttpClient(response));

        Mock<ILoggingCommunicationService> mockLoggingService = 
            new Mock<ILoggingCommunicationService>();

        var sut = new ShopBackendApiService(
            mockHttpClientFactory.Object, mockLoggingService.Object);


        var productToBeUpdated = new ProductViewModel { 
            Id = 999999, 
            Price = 10, 
            Name = "Test product" 
        };

        bool responseTask = await sut.UpdateProduct(productToBeUpdated);

        Assert.True(responseTask);
    }

    [Fact]
    public async Task UpdateProduct_Should_Return_False_When_Update_Was_Not_Successfull()
    {
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.InternalServerError
        };

        Mock<IHttpClientFactory> mockHttpClientFactory = 
            new Mock<IHttpClientFactory>();
        mockHttpClientFactory.Setup(e => e.CreateClient(string.Empty)).Returns(MockHttpClient(response));

        Mock<ILoggingCommunicationService> mockLoggingService = 
            new Mock<ILoggingCommunicationService>();

        var sut = new ShopBackendApiService(
            mockHttpClientFactory.Object, mockLoggingService.Object);


        var productToBeUpdated = new ProductViewModel { 
            Id = 999999, 
            Price = 10, 
            Name = "Test product" 
        };

        bool responseTask = await sut.UpdateProduct(productToBeUpdated);

        Assert.True(!responseTask);
    }

    [Fact]
    public async Task UpdateProduct_Should_Log_When_Update_Was_Successfull()
    {
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK
        };

        Mock<IHttpClientFactory> mockHttpClientFactory = 
            new Mock<IHttpClientFactory>();
        mockHttpClientFactory.Setup(e => e.CreateClient(string.Empty)).Returns(MockHttpClient(response));

        Mock<ILoggingCommunicationService> mockLoggingService = 
            new Mock<ILoggingCommunicationService>();

        var sut = new ShopBackendApiService(
            mockHttpClientFactory.Object, mockLoggingService.Object);


        var productToBeUpdated = new ProductViewModel { 
            Id = 999999, 
            Price = 10, 
            Name = "Test product" 
        };

        bool responseTask = await sut.UpdateProduct(productToBeUpdated);

        Assert.True(responseTask);
        mockLoggingService.Verify(
            e => e.LogTracing(It.IsAny<LogRequest>()), Times.Once());
    }

    private static HttpClient MockHttpClient(
        HttpResponseMessage response)
    {
        var mockMessageHandler = new Mock<HttpMessageHandler>();
        mockMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        return new HttpClient(mockMessageHandler.Object);
    }
}