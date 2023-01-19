using System;

namespace LoggingCommunicationLibrary.Dto;

public record struct InfoLogRequest(
    string applicationName,
    string message,
    DateTime dateTime);