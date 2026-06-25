using System.Text.Json;

namespace WeatherApp{
public class BuienradarService
{
    private readonly BuienradarClient _client;

    public BuienradarService(BuienradarClient client)
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

    public async Task<List<StationMeasurement>> GetCityInformation(string city)
        {
            var data = await _client.FetchAsync();
            var cityData = data.Actual.StationMeasurements
            .Where(s=>s.StationName.Contains(city, StringComparison.OrdinalIgnoreCase))
            .ToList();

            return cityData;
        }
}
}