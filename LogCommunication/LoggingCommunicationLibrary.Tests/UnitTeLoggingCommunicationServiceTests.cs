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
        
        var sut = new LoggingCommunicationService();
        sut.SetHttpClient(new HttpClient(mockMessageHandler.Object));
        
        var request = new LogRequest("ApplicationName",
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
        
        var sut = new LoggingCommunicationService();
        sut.SetHttpClient(new HttpClient(mockMessageHandler.Object));
        
        var request = new LogRequest("ApplicationName",
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
        
        var sut = new LoggingCommunicationService();
        sut.SetHttpClient(new HttpClient(mockMessageHandler.Object));
        
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
        
        var sut = new LoggingCommunicationService();
        sut.SetHttpClient(new HttpClient(mockMessageHandler.Object));
        
        var request = new ErrorLogRequest("ApplicationName",
            "message",
            "InnerMessage",
            "StackTrace",
            DateTime.Now);

        bool responseTask = await sut.LogError(request);
        Assert.True(!responseTask);
    }

    [Fact]
    public async Task LoggingCommunicationService_Should_Throw_InvalidOperationException_If_HttpClient_Is_Null()
    {
        var sut = new LoggingCommunicationService();

        var request = new ErrorLogRequest("ApplicationName",
            "message",
            "InnerMessage",
            "StackTrace",
            DateTime.Now);

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => sut.LogError(request));
    }
}