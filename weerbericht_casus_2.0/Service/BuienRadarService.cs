using System.Text.Json;

namespace WeatherApp{
public class BuienRadarService
{
    private readonly BuienradarClient _client;

    public BuienRadarService(BuienradarClient client)
    {
        _client = client;
    }

    public async Task<List<string>> GetCities()
    {
        var data = await _client.FetchAsync();
        var cities = data.Actual.StationMeasurements
        .Select(s=>s.StationName)
        .Distinct()
        .ToList();
        
        return cities;
    }
}
}