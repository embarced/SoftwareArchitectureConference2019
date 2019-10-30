using System;
using System.Collections.Generic;
using WeatherApp.Common;
using Microsoft.AspNetCore.Mvc;

namespace WeatherApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private static string apiKey = "197da5e10fb1ebbb22f1959434f629b8";

        private OpenWeatherMap.OpenWeatherMapClient openWeatherClient = new OpenWeatherMap.OpenWeatherMapClient(apiKey);

        // GET api/weather
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Add city to API call (e.g. /api/weather/London)" };
        }

        // GET api/weather/london
        [HttpGet("{city}")]
        public ActionResult<Weather> Get(string city)
        {
            Console.WriteLine("Call Get; city:" + city);

            var response = openWeatherClient.CurrentWeather.GetByName(city, OpenWeatherMap.MetricSystem.Metric).Result;
            var weather = GetWeatherFromResponse(response);

            return weather;
        }

        private Weather GetWeatherFromResponse(OpenWeatherMap.CurrentWeatherResponse currentWeather)
        {
            return new Weather()
            {
                City = currentWeather.City.Name,
                Temperature = new Temperature(currentWeather.Temperature.Value),
                Value = currentWeather.Weather.Value
            };
        }
    }
}
