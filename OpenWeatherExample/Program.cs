using OpenWeatherSDK;
using OpenWeatherSDK.Enums;

// Get an instance of the OpenWeatherAPI class by calling the static method GetInstance(unique API key, operating mode)
OpenWeatherAPI openWeatherAPIonDemand = OpenWeatherAPI.GetInstance("977b4da3e1eb5f66158ebd2dafa33480", ModeApi.ForRequest);

// The instance has one unique method, GetCurrentWeatherByCityNameAsync, which takes the city name as an argument and returns a string in JSON format
string buzulukWeather = await openWeatherAPIonDemand.GetCurrentWeatherByCityNameAsync("Buzuluk");
Console.WriteLine($"Buzuluk : \n{buzulukWeather}\n");

// Get an instance of the OpenWeatherAPI class by calling the static method GetInstance(unique API key, operating mode)
OpenWeatherAPI openWeatherAPIpolling = OpenWeatherAPI.GetInstance("a2c28ed5c2167b0bed93e4b8c9aa9ac5", ModeApi.Polling);

// The instance has one unique method, GetCurrentWeatherByCityNameAsync, which takes the city name as an argument and returns a string in JSON format
string spbWeather = await openWeatherAPIpolling.GetCurrentWeatherByCityNameAsync("Saint-Petersburg");
Console.WriteLine($"Saint-Petersburg : \n{spbWeather}");

Console.ReadLine();