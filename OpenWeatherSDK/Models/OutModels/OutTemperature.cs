using System.Text.Json.Serialization;

namespace OpenWeatherSDK.Models.OutModels;

public record class OutTemperature
{
    public OutTemperature(double temp, double feels_like)
    {
        Temp = temp;
        FeelsLike = feels_like;
    }

    [JsonPropertyName("temp")]
    public double Temp { get; init; }

    [JsonPropertyName("feels_like")]
    public double FeelsLike { get; init; }
}
