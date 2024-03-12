using System.Text.Json.Serialization;

namespace OpenWeatherSDK.Models.InModels.Weather;

public record class InMain
{
    [JsonPropertyName("temp")]
    public double Temp { get; init; }

    [JsonPropertyName("feels_like")]
    public double FeelsLike { get; init; }

    [JsonPropertyName("temp_min")]
    public double TempMin { get; init; }

    [JsonPropertyName("temp_max")]
    public double TempMax { get; init; }

    [JsonPropertyName("pressure")]
    public long Pressure { get; init; }

    [JsonPropertyName("humidity")]
    public long Humidity { get; init; }

    [JsonPropertyName("sea_level")]
    public long SeaLevel { get; init; }

    [JsonPropertyName("grnd_level")]
    public long GrndLevel { get; init; }
}
