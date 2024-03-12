using OpenWeatherSDK.Enums;
using OpenWeatherSDK.Models.InModels;
using OpenWeatherSDK.Models.InModels.Weather;
using System.Net.Http.Json;
using System.Text.Json;

namespace OpenWeatherSDK.HTTP;

/// <summary>
/// Class for interaction via HTTP
/// </summary>
internal class OpenWeatherApiHttp : IDisposable
{
    private readonly HttpClient _httpClient;
    private bool _isDisposed = false;

    internal OpenWeatherApiHttp() : this(new HttpClient(new WeatherExceptionHandler()))
    {
    }
    private OpenWeatherApiHttp(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Async calling one of the APIs
    /// </summary>
    /// <param name="apiKey">Unique API key</param>
    /// <param name="weatherInCity">Object with links to weather and city</param>
    /// <param name="typeApi">Type of API called</param>
    /// <param name="cancellationToken">Сancellation token</param>
    /// <returns>Object with links to weather and city</returns>
    /// <exception cref="NotImplementedException">When the method and case for the API are not implementedl</exception>
    internal async Task<WeatherInCity> CallApiAsync(string apiKey, WeatherInCity weatherInCity, ApiForCall typeApi, CancellationToken cancellationToken)
    {
        switch (typeApi)
        {
            case ApiForCall.Geocoding:
                {
                    return await CallGeocodingApiAsync(apiKey, weatherInCity, cancellationToken);
                };
            case ApiForCall.OpenWeather:
                {
                    return await CallOpenWeatherApiAsync(apiKey, weatherInCity, cancellationToken);
                }
            default: throw new NotImplementedException("typeApi does not match the TypeApi");
        }
    }

    /// <summary>
    /// Async GeocodingApi call
    /// </summary>
    /// <param name="apiKey">Unique API key</param>
    /// <param name="weatherInCity">Object with links to weather and city</param>
    /// <param name="cancellationToken">Сancellation token</param>
    /// <returns>Object with links to weather and city</returns>
    private async Task<WeatherInCity> CallGeocodingApiAsync(string apiKey, WeatherInCity weatherInCity, CancellationToken cancellationToken)
    {
        using HttpRequestMessage httpRequest = new(HttpMethod.Get, UriHelper.GetGeocodeLocationUri(apiKey, weatherInCity.CityRootObject));
        using HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken);
        weatherInCity.CityRootObject = (await httpResponse.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<List<InCityRootObject>>(cancellationToken)).FirstOrDefault();
        return weatherInCity;
    }

    /// <summary>
    /// Async OpenWeatherApi call
    /// </summary>
    /// <param name="apiKey">Unique API key</param>
    /// <param name="weatherInCity">Object with links to weather and city</param>
    /// <param name="cancellationToken">Сancellation token</param>
    /// <returns>Object with links to weather and city</returns>
    private async Task<WeatherInCity> CallOpenWeatherApiAsync(string apiKey, WeatherInCity weatherInCity, CancellationToken cancellationToken)
    {
        using HttpRequestMessage httpRequest = new(HttpMethod.Get, UriHelper.GetWeatherBaseUri(apiKey, weatherInCity.CityRootObject));
        using HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken);
        weatherInCity.WeatherRootObject = await httpResponse.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<InWeatherRootObject>(cancellationToken);
        return weatherInCity;
    }


    /// <summary>
    /// Sync calling one of the APIs, for PostEvictionDelegate
    /// </summary>
    /// <param name="apiKey">Unique API key</param>
    /// <param name="weatherInCity">Object with links to weather and city</param>
    /// <param name="typeApi">Type of API called</param>
    /// <returns>Object with links to weather and city</returns>
    /// <exception cref="NotImplementedException">When the method and case for the API are not implementedl</exception>
    internal WeatherInCity CallApi(string apiKey, WeatherInCity weatherInCity, ApiForCall typeApi)
    {
        switch (typeApi)
        {
            case ApiForCall.Geocoding:
                {
                    return CallGeocodingApi(apiKey, weatherInCity);
                };
            case ApiForCall.OpenWeather:
                {
                    return CallOpenWeatherApi(apiKey, weatherInCity);
                }
            default: throw new NotImplementedException("typeApi does not match the TypeApi");
        }
    }

    /// <summary>
    /// sync GeocodingApi call, for PostEvictionDelegate
    /// </summary>
    /// <param name="apiKey">Unique API key</param>
    /// <param name="weatherInCity">Object with links to weather and city</param>
    /// <param name="typeApi">Type of API called</param>
    /// <returns>Object with links to weather and city</returns>
    /// <exception cref="NotImplementedException">When the method and case for the API are not implementedl</exception>
    private WeatherInCity CallGeocodingApi(string apiKey, WeatherInCity weatherInCity)
    {
        using HttpRequestMessage httpRequest = new(HttpMethod.Get, UriHelper.GetGeocodeLocationUri(apiKey, weatherInCity.CityRootObject));
        using HttpResponseMessage httpResponse = _httpClient.Send(httpRequest);
        weatherInCity.CityRootObject = JsonSerializer.Deserialize<List<InCityRootObject>>(httpResponse.EnsureSuccessStatusCode().Content.ReadAsStream()).FirstOrDefault();
        return weatherInCity;
    }

    /// <summary>
    /// Sync OpenWeatherApi call, for PostEvictionDelegate
    /// </summary>
    /// <param name="apiKey">Unique API key</param>
    /// <param name="weatherInCity">Object with links to weather and city</param>
    /// <param name="typeApi">Type of API called</param>
    /// <returns>Object with links to weather and city</returns>
    /// <exception cref="NotImplementedException">When the method and case for the API are not implementedl</exception>
    private WeatherInCity CallOpenWeatherApi(string apiKey, WeatherInCity weatherInCity)
    {
        using HttpRequestMessage httpRequest = new(HttpMethod.Get, UriHelper.GetWeatherBaseUri(apiKey, weatherInCity.CityRootObject));
        using HttpResponseMessage httpResponse = _httpClient.Send(httpRequest);
        weatherInCity.WeatherRootObject = JsonSerializer.Deserialize<InWeatherRootObject>(httpResponse.EnsureSuccessStatusCode().Content.ReadAsStream());
        return weatherInCity;
    }

    public void Dispose()
    {
        Dispose(true);

    }
    protected void Dispose(bool disposing)
    {
        if (_isDisposed)
        {
            return;
        }
        if (disposing)
        {
            _httpClient.Dispose();
        }
        _isDisposed = true; 
    }

    ~OpenWeatherApiHttp()
    {
        Dispose(false);        
    }
}