using System.Text.Json.Serialization;

namespace OpenWeatherSDK.Models.InModels.Weather;

public record class InClouds
{
    [JsonPropertyName("all")]
    public long All { get; init; }
}
