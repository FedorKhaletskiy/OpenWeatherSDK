using System.Text.Json.Serialization;

namespace OpenWeatherSDK.Models.OutModels;

public record class OutWind
{
    public OutWind(double speed)
    {
        Speed = speed;
    }

    [JsonPropertyName("speed")]
    public double Speed { get; init; }
}
