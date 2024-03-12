using OpenWeatherSDK.Enums;

namespace OpenWeatherSDK.Tests.OnDemand;

public class Instance
{
    [Fact]
    public void CreateInstanceOpenWeatherApiSDK_withGoodFormatKeyOnDemandType()
    {
        using OpenWeatherApi? openWeatherApi = OpenWeatherApi.GetInstance(Consts._keyForTestsGoodValue, TypeSDK.OnDemand);
        Assert.NotNull(openWeatherApi);
    }



    [Fact]
    public void CreateInstanceOpenWeatherApiSDK_withBadFormatKeyOnDemandType()
    {
        Assert.Throws<ArgumentException>(() =>  OpenWeatherApi.GetInstance(Consts._keyForTestsBadFormat, TypeSDK.OnDemand));
    }
}
