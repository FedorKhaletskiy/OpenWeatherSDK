using OpenWeatherSDK.Models.InModels;

namespace OpenWeatherSDK.HTTP;

internal static class UriHelper
{
    private static readonly string BaseUri = "https://api.openweathermap.org";
    private static readonly string WeatherBaseUri = $"{BaseUri}/data/2.5/weather";
    private static readonly string GeocodeBaseUri = $"{BaseUri}/geo/1.0";


    private const int LIMITLOCATION = 1;
    private const string LANGUAGE = "en";

    /// <summary>
    /// Generation of URI for GeocodingApi
    /// </summary>
    /// <param name="apiKey">Unique API key</param>
    /// <param name="inCity">Model city</param>
    /// <returns></returns>
    public static string GetGeocodeLocationUri(string apiKey, InCityRootObject inCity)
    {
        return string.Concat(GeocodeBaseUri, $"/direct?q={inCity.Name}&limit={LIMITLOCATION}&appid={apiKey}");
    }

    /// <summary>
    /// Generation of URI for OpenWeatherApi
    /// </summary>
    /// <param name="apiKey">Unique API key</param>
    /// <param name="inCity">Model city</param>
    /// <returns></returns>
    public static string GetWeatherBaseUri(string apiKey, InCityRootObject inCity)
    {
        return string.Concat(WeatherBaseUri, $"?lat={inCity.Latitude}&lon={inCity.Longitude}&lang={LANGUAGE}&appid={apiKey}");
    }

}
