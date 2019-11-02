using System;
using WeatherApp.Common;

namespace WeatherApp.API.BusinessLogic
{
    public static class WeatherConverter
    {
        public static Weather GetWeatherFromResponse(OpenWeatherMap.CurrentWeatherResponse currentWeather)
        {
            var weather = new Weather()
            {
                City = currentWeather.City.Name,
                Temperature = new Temperature(currentWeather.Temperature.Value),
                Value = currentWeather.Weather.Value,
                ASCIIWeatherRepresentation = GetASCIIWeatherRepresentation(currentWeather)
            };

            return weather;
        }

        private static ASCIIWeatherRepresentation GetASCIIWeatherRepresentation(OpenWeatherMap.CurrentWeatherResponse currentWeather)
        {
            string asciiCode = ASCIIWeatherMap.GetASCIIFromOpenWeatherResponse(currentWeather);

            return new ASCIIWeatherRepresentation(asciiCode);
        }
    }
}
