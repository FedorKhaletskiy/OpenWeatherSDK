using System.Text.Json.Serialization;

namespace OpenWeatherSDK.Models.InModels.Weather;

public record class InWeatherRootObject
{
    [JsonPropertyName("coord")]
    public InCoord Coord { get; init; }

    [JsonPropertyName("weather")]
    public List<InWeather> Weathers { get; init; }

    [JsonPropertyName("base")]
    public string Base { get; init; }

    [JsonPropertyName("main")]
    public InMain Main { get; init; }

    [JsonPropertyName("visibility")]
    public long Visibility { get; init; }

    [JsonPropertyName("wind")]
    public InWind Wind { get; init; }

    [JsonPropertyName("rain")]
    public InRain Rain { get; init; }

    [JsonPropertyName("clouds")]
    public InClouds Clouds { get; init; }

    [JsonPropertyName("dt")]
    public long Dt { get; init; }

    [JsonPropertyName("sys")]
    public InSys Sys { get; init; }

    [JsonPropertyName("timezone")]
    public long Timezone { get; init; }

    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("cod")]
    public long Cod { get; init; }
}