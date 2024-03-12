using OpenWeatherSDK.Enums;

namespace OpenWeatherSDK.Tests.Polling;

public class Instance
{
    [Fact]
    public void CreateInstanceOpenWeatherApiSDK_withGoodFormatKeyPollingType()
    {
        using OpenWeatherApi? openWeatherApi = OpenWeatherApi.GetInstance(Consts._keyForTestsGoodValue, TypeSDK.Polling);
        Assert.NotNull(openWeatherApi);
    }

    [Fact]
    public void CreateInstanceOpenWeatherApiSDK_withBadFormatKeyPollingType()
    {
        Assert.Throws<ArgumentException>(() => OpenWeatherApi.GetInstance(Consts._keyForTestsBadFormat, TypeSDK.Polling));
    }
}
