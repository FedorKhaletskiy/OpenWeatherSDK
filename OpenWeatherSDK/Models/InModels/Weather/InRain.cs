using System.Text.Json.Serialization;

namespace OpenWeatherSDK.Models.InModels.Weather;

public record class InRain
{
    [JsonPropertyName("1h")]
    public double The1H { get; init; }
}
