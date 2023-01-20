using LoggingCommunicationLibrary.Dto;

namespace LoggingCommunicationLibrary.Service;

public interface ILoggingCommunicationService 
{
    void SetHttpClient(HttpClient httpClient);
    Task<bool> LogInformation(LogRequest request);
    Task<bool> LogTracing(LogRequest request);
    Task<bool> LogWarning(LogRequest request);
    Task<bool> LogError(ErrorLogRequest request);
}