using OpenWeatherSDK.Enums;

namespace OpenWeatherSDK;

public interface IOpenWeatherApi
{
    Task<string> GetCurrentWeatherByCityNameAsync(string cityName, CancellationToken cancellationToken);
}