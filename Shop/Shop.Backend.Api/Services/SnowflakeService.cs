using Shop.Backend.Api.Models;

namespace Shop.Backend.Api.Services;

public class SnowflakeService : ISnowflakeService
{
    public async Task<SnowflakeIdViewModel> SnowflakeId()
    {
        SnowflakeIdViewModel snowflakeId = new SnowflakeIdViewModel(null, false);
        
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://snowflake-factory:80/");
            
            try
            {
                var responseTask = await client.PostAsync("snowflake-id", null);
                responseTask.EnsureSuccessStatusCode();

                if (responseTask.IsSuccessStatusCode) {
                    var snowflakeResponse = await responseTask.Content.ReadFromJsonAsync<SnowflakeIdViewModel>();

                    if(snowflakeResponse != null)
                        snowflakeId = snowflakeResponse;
                }
                    
            }  
            catch (HttpRequestException ex) {
                //TODO: add log here
            }

            return snowflakeId;
        }
    }
}