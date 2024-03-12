using Microsoft.Extensions.Caching.Memory;
using OpenWeatherSDK.Enums;
using OpenWeatherSDK.HTTP;
using OpenWeatherSDK.Models.InModels;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace OpenWeatherSDK;

public class OpenWeatherApi : IOpenWeatherApi, IDisposable
{
    private static readonly Regex _apiKeyRegex = new(@"^[0-9a-f]{32}$", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
    private readonly string _apiKey;
    private readonly TypeSDK _typeSDK;
    private readonly OpenWeatherApiHttp _openWeatherApiHttp;
    private readonly IMemoryCache _memoryCache;
    private readonly ConcurrentQueue<string> _keys;
    // weather relevance
    private const int INCACHETIMESECONDS = 599;

    private bool _isDisposed = false;

    private static readonly ConcurrentDictionary<string, OpenWeatherApi> instances = new();

    /// <summary>
    /// Get or create an instance OpenWeatherApi
    /// </summary>
    /// <param name="apiKey">Unique API key</param>
    /// <param name="mode">Types of behavior</param>
    /// <returns>Returns an instance of an object if this is the first time an instance has been received or an instance has previously been created with the passed API key</returns>
    /// <exception cref="ArgumentException">If API key does not match the OpenWeatherApi key-format</exception>
    public static OpenWeatherApi GetInstance(string apiKey, TypeSDK mode)
    {
        if (!_apiKeyRegex.IsMatch(apiKey))
        {
            throw new ArgumentException("The key does not match the OpenWeatherApi key-format");
        }
        if (!instances.TryGetValue(apiKey, out OpenWeatherApi openWeatherAPI))
        {
            OpenWeatherApi newOpenWeatherAPI = new(apiKey, mode);

            instances.TryAdd(apiKey, newOpenWeatherAPI);
            return newOpenWeatherAPI;
        }
        return openWeatherAPI;
    }

    /// <summary>
    /// External constructor
    /// </summary>
    /// <param name="apiKey">Unique API key</param>
    /// <param name="typeSDK">Type of behavoir</param>
    private OpenWeatherApi(string apiKey, TypeSDK typeSDK) :
        this(apiKey,
            typeSDK,
            new OpenWeatherApiHttp(),
            new MemoryCache(new MemoryCacheOptions() { TrackStatistics = true }),
            new ConcurrentQueue<string>())
    {
    }

    private OpenWeatherApi(string apiKey, TypeSDK mode, OpenWeatherApiHttp openWeatherApiHttp, IMemoryCache memoryCache, ConcurrentQueue<string> keys)
    {
        ArgumentNullException.ThrowIfNull(apiKey);
        ArgumentNullException.ThrowIfNull(openWeatherApiHttp);
        //
        _typeSDK = mode;
        _apiKey = apiKey;
        _openWeatherApiHttp = openWeatherApiHttp;
        _memoryCache = memoryCache;
        _keys = keys;
    }

    /// <summary>
    /// Returns a json serialized string containing the current weather in the specified city
    /// </summary>
    /// <param name="cityName">Name of city</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    public async Task<string> GetCurrentWeatherByCityNameAsync(string cityName, CancellationToken cancellationToken)
    {
        WeatherInCity weatherInCity;

        // to control remove the first city when adding the eleventh
        string queueKey = cityName;

        // if a city is not in the queue, then it is definitely not in the cache
        if (_keys.TryPeek(out queueKey))
        {
            // but if the city is in the queue, it may not be in the cache (10 minutes have elapsed)
            if (_memoryCache.TryGetValue(cityName, out object value) && value is WeatherInCity weatherInCityCache)
            {
                ArgumentNullException.ThrowIfNull(weatherInCityCache);
                ArgumentNullException.ThrowIfNull(weatherInCityCache.WeatherRootObject);
                return JsonSerializer.Serialize(weatherInCityCache.ToOutputObject());
            }
        }
        MemoryCacheEntryOptions options = new()
        {
            // weather relevance
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(INCACHETIMESECONDS),
        };

        // registering a delegate that is triggered when removal from cache
        if (_typeSDK == TypeSDK.Polling)
        {
            _ = options.RegisterPostEvictionCallback(OnPostEvictionModePolling);
        }

        // add the eleventh city remove the first
        _keys.Enqueue(cityName);
        if (_keys.Count > 10)
        {
            queueKey = _keys.First();
            _keys.TryDequeue(out queueKey);
            _memoryCache.Remove(queueKey);
        }

        weatherInCity = new() { CityRootObject = new() { Name = cityName } };
        try
        {
            weatherInCity = await GetCityInfoByNameAsync(weatherInCity, cancellationToken);
            weatherInCity = await _openWeatherApiHttp.CallApiAsync(_apiKey, weatherInCity, ApiForCall.OpenWeather, cancellationToken);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        _memoryCache.Set(cityName, weatherInCity, options);

        ArgumentNullException.ThrowIfNull(weatherInCity);
        ArgumentNullException.ThrowIfNull(weatherInCity.WeatherRootObject);
        return JsonSerializer.Serialize(weatherInCity.ToOutputObject());
    }

    /// <summary>
    /// Method that is triggered when removal from cache
    /// </summary>
    private void OnPostEvictionModePolling(
        object key, object value, EvictionReason reason, object state)
    {
        if (value is WeatherInCity weatherInCity)
        {
            try
            {
                weatherInCity = _openWeatherApiHttp.CallApi(_apiKey, weatherInCity, ApiForCall.OpenWeather);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    /// <summary>
    /// GeoCodingApi call with checks
    /// </summary>
    /// <param name="weatherInCity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<WeatherInCity> GetCityInfoByNameAsync(WeatherInCity weatherInCity, CancellationToken cancellationToken)
    {
        weatherInCity = await  _openWeatherApiHttp.CallApiAsync(_apiKey, weatherInCity, ApiForCall.Geocoding, cancellationToken);
        ArgumentNullException.ThrowIfNull(weatherInCity);
        ArgumentNullException.ThrowIfNull(weatherInCity.CityRootObject);
        return weatherInCity;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected void Dispose(bool disposing)
    {
        if (_isDisposed)
        {
            return;
        }
        if (disposing)
        {
            _openWeatherApiHttp.Dispose();
        }
        _isDisposed = true;
    }
    ~OpenWeatherApi()
    {
        Dispose(false);
    }
}