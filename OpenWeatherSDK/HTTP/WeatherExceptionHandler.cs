using OpenWeatherSDK.HTTP.Exceptions;
using System.Net;

namespace OpenWeatherSDK.HTTP;

internal class WeatherExceptionHandler : DelegatingHandler, IDisposable
{
    private bool _isDisposed = false;
    public WeatherExceptionHandler() : base(new HttpClientHandler())
    {        
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.InternalServerError
                || response.StatusCode == HttpStatusCode.BadGateway
                || response.StatusCode == HttpStatusCode.ServiceUnavailable
                || response.StatusCode == HttpStatusCode.GatewayTimeout)
            {
                throw new WeatherException("In case you receive one of the following errors 500, 502, 503 or 504 please contact us for assistance. Please enclose an example of your API request that receives this error into your email to let us analyze it and find a solution for you promptly.");
            }
            else
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized: throw new WeatherException("You can get the error 401 in the following cases:\r\n\r\nYou did not specify your API key in API request.\r\nYour API key is not activated yet. Within the next couple of hours, it will be activated and ready to use.\r\nYou are using wrong API key in API request. Please, check your right API key in personal account.\r\nYou are using a Free subscription and try requesting data available in other subscriptions . For example, 16 days/daily forecast API, any historical weather data, Weather maps 2.0, etc). Please, check your subscription in your personal account.");
                    case HttpStatusCode.NotFound: throw new WeatherException("You can get this error when you specified the wrong city name, ZIP-code or city ID. For your reference, this list contains City name, City ID, Geographical coordinates of the city (lon, lat), Zoom, etc.\r\n\r\nYou can also get the error 404 if the format of your API request is incorrect. In this case, please review it and check for any mistakes. To see examples of correct API requests, please visit the Documentation of a specific API and read the examples of API calls there.");
                    case HttpStatusCode.TooManyRequests: throw new WeatherException("You can recieve this error in the following cases:\r\n\r\nIf you have a Free plan of Professional subscriptions and make more than 60 API calls per minute (surpassing the limit of your subscription). To avoid this situation, please consider upgrading to a subscription plan that meets your needs or reduce the number of API calls in accordance with the limits.");
                }
            }
            throw new Exception($"{response.StatusCode}");
        }
        return response;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected void Dispose(bool disposing)
    {
        if (_isDisposed)
        {
            return;
        }
        if (disposing)
        {
            base.Dispose();
        }
        _isDisposed = true;
    }

    ~WeatherExceptionHandler()
    {
        Dispose(false);
    }
}