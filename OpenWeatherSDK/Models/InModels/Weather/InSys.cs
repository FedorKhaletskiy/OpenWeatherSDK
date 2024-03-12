using System.Text.Json.Serialization;

namespace OpenWeatherSDK.Models.InModels.Weather;

public record class InSys
{
    [JsonPropertyName("type")]
    public long Type { get; init; }

    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("country")]
    public string Country { get; init; }

    [JsonPropertyName("sunrise")]
    public long Sunrise { get; init; }

    [JsonPropertyName("sunset")]
    public long Sunset { get; init; }
}
