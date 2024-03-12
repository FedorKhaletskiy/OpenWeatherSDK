# OpenWeatherAPI C# SDK
## Introduction

OpenWeatherAPI SDK a software development kit (SDK) that can be used by other developers to easily access a weather API and retrieve weather data for a given location.

The SDK stores weather information in the requested cities and, if it is current, will return the stored value (Weather is considered current if less than 10 minutes have passed)
To save memory, the SDK stores information for no more than 10 cities at a time.

OpenWeatherAPI SDK has two types of behavior: 
- on-demand: the SDK updates the weather information only on customer requests.
- polling mode: the SDK requests new weather information for all stored locations to have zero-latency response for customer requests.

## Contents

- [Installation](#title1)
- [Preparation](#title2)
- [Usage Example](#title3)

## <a id="title1">Installation</a>

- .NET CLI `dotnet add package FKhaletskiy.SDK.OpenWeatherApiSDK --version 1.0.0 `
- Package Manager `NuGet\Install-Package FKhaletskiy.SDK.OpenWeatherApiSDK -Version 1.0.0`
- PackageReference `<PackageReference Include="FKhaletskiy.SDK.OpenWeatherApiSDK" Version="1.0.0" />`
- Or you can use "Manage NuGet çackages" in the Microsoft Visual Studio UI

## <a id="title2">Preparation</a>

- Register on the site `https://openweathermap.org/`
- Get a unique API key
- Add Nuget SDK Package to your project

## <a id="title3">Usage Example</a>

```csharp
// enter your unique API key
string yourApiKey = "...";

// Get an instance of the OpenWeatherAPI class by calling the static method GetInstance(unique API key, operating mode)
OpenWeatherAPI openWeatherAPIonDemand = OpenWeatherAPI.GetInstance(yourApiKey, ModeApi.ForRequest);

// The instance has one unique method, GetCurrentWeatherByCityNameAsync, which takes the city name as an argument and returns a string in JSON format
string buzulukWeather = await openWeatherAPIonDemand.GetCurrentWeatherByCityNameAsync("Buzuluk");
Console.WriteLine($"Buzuluk : \n{buzulukWeather}\n");

// Get an instance of the OpenWeatherAPI class by calling the static method GetInstance(unique API key, operating mode)
OpenWeatherAPI openWeatherAPIpolling = OpenWeatherAPI.GetInstance(yourApiKey, ModeApi.Polling);

// The instance has one unique method, GetCurrentWeatherByCityNameAsync, which takes the city name as an argument and returns a string in JSON format
string spbWeather = await openWeatherAPIpolling.GetCurrentWeatherByCityNameAsync("Saint-Petersburg");
Console.WriteLine($"Saint-Petersburg : \n{spbWeather}");
```