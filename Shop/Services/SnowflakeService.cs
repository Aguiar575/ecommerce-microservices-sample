using Shop.Models;

namespace Shop.Services;

public class SnowflakeService
{
    public async Task<SnowflakeIdViewModel?> SnowflakeId()
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://localhost:7200/");
            
            try
            {
                var responseTask = await client.PostAsync("snowflake-id", null);
                responseTask.EnsureSuccessStatusCode();

                if (responseTask.IsSuccessStatusCode)
                    return await responseTask.Content.ReadFromJsonAsync<SnowflakeIdViewModel>();
            }  
            catch (HttpRequestException ex) {
                //TODO: add log here
            }

            return new SnowflakeIdViewModel(null, false);
        }
    }
}