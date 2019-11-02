using System;
using System.Collections.Generic;
using WeatherApp.Common;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.API.BusinessLogic;

namespace WeatherApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private static string apiKey = "ab8a963e9c07fabcb94c3b999c26d9aa";

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
            var weather = WeatherConverter.GetWeatherFromResponse(response);

            return weather;
        }


    }
}
