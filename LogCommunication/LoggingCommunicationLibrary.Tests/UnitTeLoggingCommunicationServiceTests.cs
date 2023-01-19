using System.Net;
using LoggingCommunicationLibrary.Dto;
using LoggingCommunicationLibrary.Service;
using Moq.Protected;

namespace LoggingCommunicationLibrary.Tests;

public class UnitTeLoggingCommunicationServiceTests
{
    [Fact]
    public async Task LogInformation_Should_Return_True()
    {
        var mockMessageHandler = new Mock<HttpMessageHandler>();
        mockMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", 
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage {
                StatusCode = HttpStatusCode.OK
            });
        
        var sut = new LoggingCommunicationService(
            new HttpClient(mockMessageHandler.Object));
        
        var request = new InfoLogRequest("ApplicationName",
            "message",
            DateTime.Now);

        bool responseTask = await sut.LogInformation(request);
        Assert.True(responseTask);
    }
    
    [Fact]
    public async Task LogInformation_Should_Return_False()
    {
        var mockMessageHandler = new Mock<HttpMessageHandler>();
        mockMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", 
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage {
                StatusCode = HttpStatusCode.InternalServerError
            });
        
        var sut = new LoggingCommunicationService(
            new HttpClient(mockMessageHandler.Object));
        
        var request = new InfoLogRequest("ApplicationName",
            "message",
            DateTime.Now);

        bool responseTask = await sut.LogInformation(request);
        Assert.True(!responseTask);
    }

    [Fact]
    public async Task LogError_Should_Return_True()
    {
        var mockMessageHandler = new Mock<HttpMessageHandler>();
        mockMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", 
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage {
                StatusCode = HttpStatusCode.OK
            });
        
        var sut = new LoggingCommunicationService(
            new HttpClient(mockMessageHandler.Object));
        
        var request = new ErrorLogRequest("ApplicationName",
            "message",
            "InnerMessage",
            "StackTrace",
            DateTime.Now);

        bool responseTask = await sut.LogError(request);
        Assert.True(responseTask);
    }
    
    [Fact]
    public async Task LogError_Should_Return_False()
    {
        var mockMessageHandler = new Mock<HttpMessageHandler>();
        mockMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", 
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage {
                StatusCode = HttpStatusCode.InternalServerError
            });
        
        var sut = new LoggingCommunicationService(
            new HttpClient(mockMessageHandler.Object));
        
        var request = new ErrorLogRequest("ApplicationName",
            "message",
            "InnerMessage",
            "StackTrace",
            DateTime.Now);

        bool responseTask = await sut.LogError(request);
        Assert.True(!responseTask);
    }
}