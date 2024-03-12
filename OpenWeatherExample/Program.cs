using OpenWeatherSDK;
using OpenWeatherSDK.Enums;

string yourApiKey = "...";
string firstCity = "Buzuluk";
string secondCity = "Saint-Petersburg";

// Get an instance of the OpenWeatherAPI class by calling the static method GetInstance(unique API key, operating mode)
OpenWeatherAPI openWeatherAPIonDemand = OpenWeatherAPI.GetInstance(yourApiKey, ModeApi.ForRequest);

// The instance has one unique method, GetCurrentWeatherByCityNameAsync, which takes the city name as an argument and returns a string in JSON format
string firstkWeather = await openWeatherAPIonDemand.GetCurrentWeatherByCityNameAsync(firstCity);
Console.WriteLine($"{firstCity} : \n{firstkWeather}\n");

// Get an instance of the OpenWeatherAPI class by calling the static method GetInstance(unique API key, operating mode)
OpenWeatherAPI openWeatherAPIpolling = OpenWeatherAPI.GetInstance(yourApiKey, ModeApi.Polling);

// The instance has one unique method, GetCurrentWeatherByCityNameAsync, which takes the city name as an argument and returns a string in JSON format
string secondWeather = await openWeatherAPIpolling.GetCurrentWeatherByCityNameAsync(secondCity);
Console.WriteLine($"{secondCity} : \n{secondWeather}");

Console.ReadLine();