using System.Text.Json.Serialization;

namespace OpenWeatherSDK.Models.InModels;

public class InCityRootObject
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("lat")]
    public double Latitude { get; set; }

    [JsonPropertyName("lon")]
    public double Longitude { get; set; }
}