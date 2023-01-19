using LoggingService.Api.Dto;
using Microsoft.Extensions.Logging;

namespace LoggingService.Tests;

public class LogServiceTests
{
    [Fact]
    public async void LogService_should_Create_Information_Level_Log()
    {
        var mockIlogger = new Mock<ILogger<LogService>>();
        var sut = new LogService(mockIlogger.Object);
        var logRequest = new LogRequest(
            "TestApplication",
            "Test message",
            DateTime.Now);

        await sut.LogInformation(logRequest);

        mockIlogger.Verify(
        x => x.Log(
            It.Is<LogLevel>(l => l == LogLevel.Information),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) =>  
                v.ToString() == $"Log Level: Information ApplicationName: {logRequest.ApplicationName} " +
                $"Message: {logRequest.Message} " +
                $"Date: {logRequest.Date}"),
            It.IsAny<Exception>(),
            It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);

    }

    [Fact]
    public async void LogService_should_Create_Error_Level_Log()
    {
        var mockIlogger = new Mock<ILogger<LogService>>();
        var sut = new LogService(mockIlogger.Object);
        var logRequest = new LogRequest(
            "TestApplication",
            "Test message",
            "Inner message",
            "Stack trace",
            DateTime.Now);

        await sut.LogError(logRequest);

        mockIlogger.Verify(
        x => x.Log(
            It.Is<LogLevel>(l => l == LogLevel.Error),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) =>  
                v.ToString() == 
                    $"Log Level: Information ApplicationName: {logRequest.ApplicationName} " +
                    $"Message: {logRequest.Message} " +
                    $"InnerMessage: {logRequest.InnerMessage} " +
                    $"Stacktrace: {logRequest.StackTrace} " +
                    $"Date: {logRequest.Date}"),
            It.IsAny<Exception>(),
            It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);

    }
}