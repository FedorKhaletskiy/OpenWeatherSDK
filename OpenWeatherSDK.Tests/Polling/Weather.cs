using OpenWeatherSDK.Enums;
using OpenWeatherSDK.HTTP.Exceptions;

namespace OpenWeatherSDK.Tests.Polling;

public class Weather
{
    [Fact]
    public void GetWeather_withBadCityNamePollingType()
    {
        using CancellationTokenSource cancellationTokenSource = new();
        CancellationToken cancellationToken = cancellationTokenSource.Token;
        using OpenWeatherApi? openWeatherApi = OpenWeatherApi.GetInstance(Consts._keyForTestsGoodValue, TypeSDK.Polling);
        Assert.ThrowsAsync<WeatherException>(async () => await openWeatherApi.GetCurrentWeatherByCityNameAsync(Consts._cityNameBad, cancellationToken));
    }

    [Fact]
    public void GetWeather_withBadApiKeyPollingType()
    {
        using CancellationTokenSource cancellationTokenSource = new();
        CancellationToken cancellationToken = cancellationTokenSource.Token;
        using OpenWeatherApi? openWeatherApi = OpenWeatherApi.GetInstance(Consts._keyForTestsBadValue, TypeSDK.Polling);
        Assert.ThrowsAsync<WeatherException>(async () => await openWeatherApi.GetCurrentWeatherByCityNameAsync(Consts._cityNameGood, cancellationToken));
    }

    [Fact]
    public void GetWeather_withGoodApiKeyAndGoodCityPollingType()
    {
        using CancellationTokenSource cancellationTokenSource = new();
        CancellationToken cancellationToken = cancellationTokenSource.Token;
        using OpenWeatherApi? openWeatherApi = OpenWeatherApi.GetInstance(Consts._keyForTestsGoodValue, TypeSDK.Polling);
        Assert.ThrowsAsync<WeatherException>(async () => await openWeatherApi.GetCurrentWeatherByCityNameAsync(Consts._cityNameGood, cancellationToken));
    }
}
