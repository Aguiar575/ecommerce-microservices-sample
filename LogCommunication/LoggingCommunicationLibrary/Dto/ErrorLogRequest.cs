using System;

namespace LoggingCommunicationLibrary.Dto;

public record struct ErrorLogRequest(
    string applicationName,
    string message,
    string innerMessage,
    string stackTrace,
    DateTime dateTime);
