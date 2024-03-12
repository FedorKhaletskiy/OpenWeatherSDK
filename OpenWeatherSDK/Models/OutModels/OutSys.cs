using System.Text.Json.Serialization;

namespace OpenWeatherSDK.Models.OutModels;

public record class OutSys
{
    public OutSys(long sunrise, long sunset)
    {
        Sunrise = sunrise;
        Sunset = sunset;
    }

    [JsonPropertyName("sunrise")]
    public long Sunrise { get; init; }

    [JsonPropertyName("sunset")]
    public long Sunset { get; init; }
}
