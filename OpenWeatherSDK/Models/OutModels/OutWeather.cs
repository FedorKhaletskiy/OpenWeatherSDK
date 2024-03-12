using System.Text.Json.Serialization;

namespace OpenWeatherSDK.Models.OutModels;

public record class OutWeather
{
    public OutWeather(string main, string description)
    {
        Main = main;
        Description = description;
    }

    [JsonPropertyName("main")]
    public string Main { get; init; }

    [JsonPropertyName("description")]
    public string Description { get; init; }
}
