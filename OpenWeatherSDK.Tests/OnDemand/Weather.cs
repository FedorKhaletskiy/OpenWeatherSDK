using OpenWeatherSDK.Enums;
using OpenWeatherSDK.HTTP.Exceptions;

namespace OpenWeatherSDK.Tests.OnDemand;

public class Weather
{
    [Fact]
    public void GetWeather_withGoodApiKeyAndGoodCityOnDemandType()
    {
        using CancellationTokenSource cancellationTokenSource = new();
        CancellationToken cancellationToken = cancellationTokenSource.Token;
        using OpenWeatherApi? openWeatherApi = OpenWeatherApi.GetInstance(Consts._keyForTestsGoodValue, TypeSDK.OnDemand);
        Assert.ThrowsAsync<WeatherException>(async () => await openWeatherApi.GetCurrentWeatherByCityNameAsync(Consts._cityNameGood, cancellationToken));
    }

    [Fact]
    public void GetWeather_withBadApiKeyOnDemandType()
    {
        using CancellationTokenSource cancellationTokenSource = new();
        CancellationToken cancellationToken = cancellationTokenSource.Token;
        using OpenWeatherApi? openWeatherApi = OpenWeatherApi.GetInstance(Consts._keyForTestsBadValue, TypeSDK.OnDemand);
        Assert.ThrowsAsync<WeatherException>(async () => await openWeatherApi.GetCurrentWeatherByCityNameAsync(Consts._cityNameGood, cancellationToken));
    }

    [Fact]
    public void GetWeather_withBadCityNameOnDemandType()
    {
        using CancellationTokenSource cancellationTokenSource = new();
        CancellationToken cancellationToken = cancellationTokenSource.Token;
        using OpenWeatherApi? openWeatherApi = OpenWeatherApi.GetInstance(Consts._keyForTestsGoodValue, TypeSDK.OnDemand);
        Assert.ThrowsAsync<WeatherException>(async () => await openWeatherApi.GetCurrentWeatherByCityNameAsync(Consts._cityNameBad, cancellationToken));
    }
}
