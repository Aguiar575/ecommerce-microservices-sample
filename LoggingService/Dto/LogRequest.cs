namespace LoggingService.Dto;

public record LogRequest(
    string ApplicationName,
    string Message,
    string InnerMessage,
    string StackTrace,
    DateTime Date);
