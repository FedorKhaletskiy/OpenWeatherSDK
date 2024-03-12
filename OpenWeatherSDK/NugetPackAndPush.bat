dotnet pack -p:PackageVersion=1.0.0 -o:C:\OpenWeatherSDK\OpenWeatherSDK

dotnet nuget push FKhaletskiy.OpenWeatherApiSDK.1.0.0.nupkg -k {my_key} -s https://api.nuget.org/v3/index.json