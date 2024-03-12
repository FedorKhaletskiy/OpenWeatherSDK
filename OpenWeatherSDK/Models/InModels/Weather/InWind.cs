using System.Text.Json.Serialization;

namespace OpenWeatherSDK.Models.InModels.Weather;

public record class InWind
{
    [JsonPropertyName("speed")]
    public double Speed { get; init; }

    [JsonPropertyName("deg")]
    public long Deg { get; init; }

    [JsonPropertyName("gust")]
    public double Gust { get; init; }
}
