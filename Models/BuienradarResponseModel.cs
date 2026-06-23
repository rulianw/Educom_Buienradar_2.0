using System.Text.Json.Serialization;

public class WeatherFeed
{
    [JsonPropertyName("actual")]
    public Actual Actual { get; set; }
}

public class Actual
{
    [JsonPropertyName("stationmeasurements")]
    public List<StationMeasurement> StationMeasurements { get; set; }
}

public class StationMeasurement
{
    [JsonPropertyName("stationname")]
    public string StationName { get; set; }
}