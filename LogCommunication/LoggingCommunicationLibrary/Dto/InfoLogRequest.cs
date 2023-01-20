namespace LoggingCommunicationLibrary.Dto;

public record struct LogRequest(
    string applicationName,
    string message,
    DateTime dateTime);