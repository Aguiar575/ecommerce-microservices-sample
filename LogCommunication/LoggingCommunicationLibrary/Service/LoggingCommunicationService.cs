using System.Text.Json;
using LoggingCommunicationLibrary.Dto;

namespace LoggingCommunicationLibrary.Service;

public class LoggingCommunicationService{
    private readonly HttpClient _httpClient;
    private static string _baseUrl = "http://testing-url:80/";

    public LoggingCommunicationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_baseUrl);
    }

    public async Task<bool> LogInformation(InfoLogRequest request) =>
        await SendLogMessage(
            JsonSerializer.Serialize(request),
            "logInformation");

    public async Task<bool> LogError(ErrorLogRequest request) => 
        await SendLogMessage(
            JsonSerializer.Serialize(request),
            "logError");

    private async Task<bool> SendLogMessage(string jsonRequest, string requestAction)
    {
        var content = new StringContent(jsonRequest,
                    System.Text.Encoding.UTF8, "application/json");

        var responseTask = await _httpClient.PostAsync(requestAction, content);
        return responseTask.IsSuccessStatusCode;
    }
}