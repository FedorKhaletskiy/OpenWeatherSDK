using System.Text.Json.Serialization;

namespace OpenWeatherSDK.Models.InModels.Weather;

public record class InCoord
{
    [JsonPropertyName("lon")]
    public double Lon { get; init; }

    [JsonPropertyName("lat")]
    public double Lat { get; init; }
}
