using System.Text.Json.Serialization;
//mirroring the structure of the JSON, showing what to display

namespace WeatherApp{
    public class WeatherFeed
    {
        [JsonPropertyName("actual")]
        public Actual? Actual { get; set; }
    }

    public class Actual
    {
        [JsonPropertyName("stationmeasurements")]
        public List<StationMeasurement>? StationMeasurements { get; set; }
    }

    public class StationMeasurement
    {
        [JsonPropertyName("stationname")]
        public string? StationName { get; set; }

        [JsonPropertyName("timestamp")]
        public DateTime? Timestamp { get; set; }

        [JsonPropertyName("temperature")]
        public float Temperature { get; set; }

        [JsonPropertyName("feeltemperature")]
        public float FeelTemperature { get; set; }

        [JsonPropertyName("groundtemperature")]
        public float GroundTemperature { get; set; }

        [JsonPropertyName("sunpower")]
        public float SunPower { get; set; }

        [JsonPropertyName("rainFallLastHour")]
        public float RainFallLastHour { get; set; }

        [JsonPropertyName("winddirection")]
        public string? WindDirection { get; set; }

    }

    
}