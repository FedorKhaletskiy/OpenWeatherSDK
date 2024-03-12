using System.Text.Json.Serialization;

namespace OpenWeatherSDK.Models.OutModels;

public record OutRootObject
{
    public OutRootObject(
        OutWeather weather,
        OutTemperature temperature,
        long visibility,
        OutWind wind,
        long datetime,
        OutSys sys,
        long timezone,
        string name)
    {
        Weather = weather;
        Temperature = temperature;
        Visibility = visibility;
        Wind = wind;
        Datetime = datetime;
        Sys = sys;
        Timezone = timezone;
        Name = name;
    }

    [JsonPropertyName("weather")]
    public OutWeather Weather { get; init; }

    [JsonPropertyName("temperature")]
    public OutTemperature Temperature { get; init; }

    [JsonPropertyName("visibility")]
    public long Visibility { get; init; }

    [JsonPropertyName("wind")]
    public OutWind Wind { get; init; }

    [JsonPropertyName("datetime")]
    public long Datetime { get; init; }

    [JsonPropertyName("sys")]
    public OutSys Sys { get; init; }

    [JsonPropertyName("timezone")]
    public long Timezone { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }
}