using Shop.Backend.Api.Dto;
using Shop.Backend.Api.Models;

namespace Shop.Backend.Api.Services;

public class SnowflakeService : ISnowflakeService
{
    private static string _baseUrl = "http://snowflake-factory:80/";
    private readonly HttpClient _httpClient;

    public SnowflakeService(HttpClient httpClient) =>
        _httpClient = httpClient;

    public async Task<SnowflakeIdRequest> SnowflakeId()
    {
        SnowflakeIdRequest snowflakeId = new SnowflakeIdRequest(null, false);

        _httpClient.BaseAddress = new Uri(_baseUrl);

        var responseTask = await _httpClient.PostAsync("snowflake-id", null);
        responseTask.EnsureSuccessStatusCode();

        if (responseTask.IsSuccessStatusCode)
        {
            var snowflakeResponse = await responseTask.Content.ReadFromJsonAsync<SnowflakeIdRequest>();

            if (snowflakeResponse != null)
                snowflakeId = snowflakeResponse;
        }

        return snowflakeId;
    }
}