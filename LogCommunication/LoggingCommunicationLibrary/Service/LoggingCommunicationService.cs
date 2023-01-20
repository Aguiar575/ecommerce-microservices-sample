using System.Text.Json;
using LoggingCommunicationLibrary.Dto;

namespace LoggingCommunicationLibrary.Service;

public class LoggingCommunicationService : ILoggingCommunicationService
{
    private HttpClient? _httpClient;
    private static string _baseUrl = "http://testing-url:80/";

    public void SetHttpClient(HttpClient httpClient) 
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_baseUrl);
    }

    public async Task<bool> LogInformation(LogRequest request) =>
        await SendLogMessage(
            JsonSerializer.Serialize(request),
            "logInformation");

    public async Task<bool> LogTracing(LogRequest request) =>
        await SendLogMessage(
            JsonSerializer.Serialize(request),
            "logTracing");

    public async Task<bool> LogWarning(LogRequest request) =>
        await SendLogMessage(
            JsonSerializer.Serialize(request),
            "logWarning");

    public async Task<bool> LogError(ErrorLogRequest request) => 
        await SendLogMessage(
            JsonSerializer.Serialize(request),
            "logError");

    private async Task<bool> SendLogMessage(string jsonRequest, string requestAction)
    {
        if(_httpClient is null) 
            throw new InvalidOperationException(
                    "First, you need to set HttpClient");
        
        var content = new StringContent(jsonRequest,
                    System.Text.Encoding.UTF8, "application/json");

        var responseTask = await _httpClient.PostAsync(requestAction, content);
        return responseTask.IsSuccessStatusCode;
    }
}