using System.Text.Json.Serialization;

namespace OpenWeatherSDK.Models.InModels.Weather;

public record class InWeather
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("main")]
    public string Main { get; init; }

    [JsonPropertyName("description")]
    public string Description { get; init; }

    [JsonPropertyName("icon")]
    public string Icon { get; init; }
}
