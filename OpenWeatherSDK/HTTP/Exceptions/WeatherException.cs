namespace OpenWeatherSDK.HTTP.Exceptions;

/// <summary>
/// API exceptions
/// </summary>
public class WeatherException : Exception
{
    public WeatherException() { }
    public WeatherException(string message) : base(message) { }
}
