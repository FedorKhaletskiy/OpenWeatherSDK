using OpenWeatherSDK;
using OpenWeatherSDK.Enums;

string yourApiKey = "...";
string firstCity = "Buzuluk";
string secondCity = "Saint-Petersburg";

CancellationToken cancellationToken = new();

// Get an instance of the OpenWeatherAPI class by calling the static method GetInstance(unique API key, operating mode)
using OpenWeatherApi openWeatherAPIonDemand = OpenWeatherApi.GetInstance(yourApiKey, TypeSDK.OnDemand);

// The instance has one unique method, GetCurrentWeatherByCityNameAsync, which takes the city name as an argument and returns a string in JSON format
string firstkWeather = await openWeatherAPIonDemand.GetCurrentWeatherByCityNameAsync(firstCity, cancellationToken);
Console.WriteLine($"{firstCity} : \n{firstkWeather}\n");

// Get an instance of the OpenWeatherAPI class by calling the static method GetInstance(unique API key, operating mode)
using OpenWeatherApi openWeatherAPIpolling = OpenWeatherApi.GetInstance(yourApiKey, TypeSDK.Polling);

// The instance has one unique method, GetCurrentWeatherByCityNameAsync, which takes the city name as an argument and returns a string in JSON format
string secondWeather = await openWeatherAPIpolling.GetCurrentWeatherByCityNameAsync(secondCity, cancellationToken);
Console.WriteLine($"{secondCity} : \n{secondWeather}");

Console.ReadLine();