using OpenWeatherSDK.Models.InModels.Weather;
using OpenWeatherSDK.Models.OutModels;

namespace OpenWeatherSDK.Models.InModels;

public class WeatherInCity
{
    public InWeatherRootObject WeatherRootObject { get; set; }
    public InCityRootObject CityRootObject { get; set; }

    /// <summary>
    /// Generates 
    /// </summary>
    /// <returns>Object of out JSON</returns>
    /// <exception cref="ArgumentNullException">If the existing Weathers object is null</exception>
    public OutRootObject ToOutputObject()
    {
        InWeather weather = WeatherRootObject.Weathers.FirstOrDefault();
        return weather is null
            ? throw new ArgumentNullException("weathers")
            : new OutRootObject(
            new OutWeather(weather.Main, weather.Description),
            new OutTemperature(WeatherRootObject.Main.Temp, WeatherRootObject.Main.FeelsLike),
            WeatherRootObject.Visibility,
            new OutWind(WeatherRootObject.Wind.Speed),
            WeatherRootObject.Dt,
            new OutSys(WeatherRootObject.Sys.Sunrise, WeatherRootObject.Sys.Sunset),
            WeatherRootObject.Timezone,
            WeatherRootObject.Name);
    }
}
