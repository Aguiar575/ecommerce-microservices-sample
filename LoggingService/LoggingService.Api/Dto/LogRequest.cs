using System;

namespace LoggingService.Api.Dto;

public record LogRequest
{
    public LogRequest(
        string applicationName,
        string message,
        DateTime dateTime)
    {
        ApplicationName = applicationName;
        Message = message;
        Date = dateTime;
    }

    public LogRequest(
        string applicationName,
        string message,
        string innerMessage,
        string stackTrace,
        DateTime dateTime)
    {
        ApplicationName = applicationName;
        Message = message;
        InnerMessage = innerMessage;
        StackTrace = stackTrace;
        Date = dateTime;
    }

    public string ApplicationName {get; init; }
    public string Message {get; init; }
    public string InnerMessage {get; init; } = default!;
    public string StackTrace {get; init; } = default!;
    public DateTime Date {get; init; }
}
    
