using System.Text.Json;
using System.Net.Http;
namespace WeatherApp
{
    public class BuienradarClient
    {
        private readonly HttpClient _httpClient;

        public BuienradarClient (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherFeed> FetchAsync()
        {
            var url = "https://data.buienradar.nl/2.0/feed/json";
            var json = await _httpClient.GetStringAsync(url);
            var data = JsonSerializer.Deserialize<WeatherFeed>(json);
            return data ?? throw new InvalidOperationException("Failed to deserialize weather feed data from Buienradar.");
        }
    }
}