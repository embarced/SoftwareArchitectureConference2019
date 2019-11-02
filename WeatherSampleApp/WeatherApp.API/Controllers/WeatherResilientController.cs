using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Common;
using Microsoft.AspNetCore.Mvc;
using Polly;
using Polly.CircuitBreaker;
using Polly.Contrib.Simmy;
using Polly.Contrib.Simmy.Fault;
using Polly.Contrib.Simmy.Latency;
using Polly.Timeout;
using WeatherApp.API.BusinessLogic;

namespace WeatherApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherResilientController : ControllerBase
    {
        private static string apiKey = "197da5e10fb1ebbb22f1959434f629b8";

        private OpenWeatherMap.OpenWeatherMapClient openWeatherClient = new OpenWeatherMap.OpenWeatherMapClient(apiKey);

        // GET api/weatherresilient
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Add city to API call (e.g. /api/weatherresilient/London)" };
        }

        // GET api/weatherresilient/london
        [HttpGet("{city}")]
        public ActionResult<Weather> Get(string city)
        {
            Console.WriteLine("Call Get; city:" + city);

            Weather result = GetWeatheryWithPolly(city);

            return result;
        }
        
        private Weather GetWeatheryWithPolly(string city)
        {
            AsyncCircuitBreakerPolicy breaker = Policy
              .Handle<OpenWeatherMap.OpenWeatherMapException>()
              .CircuitBreakerAsync(
                exceptionsAllowedBeforeBreaking: 2,
                durationOfBreak: TimeSpan.FromMinutes(1)
              );

            var timeoutPolicy = Policy
                .TimeoutAsync(new TimeSpan(0, 0, 2), TimeoutStrategy.Pessimistic); // Pessimistic strategy as openWeatherClient doesn't support CancellationToken

            var fallBackPolicy = Policy<OpenWeatherMap.CurrentWeatherResponse>
                .Handle<Exception>(IsFallbackPolicyEnabled)
                .FallbackAsync<OpenWeatherMap.CurrentWeatherResponse>((ct) => DoFallbackAction(ct, city));

            var simmyPolicy = GetSimmyLatencyPolicy();

            var policyWrap = fallBackPolicy // fallbackPolicy must be the innermost to work for all cases!!
                .WrapAsync(breaker)
                .WrapAsync(timeoutPolicy)
                .WrapAsync(simmyPolicy); // simmyPolicy must be the outermost to trigger all internal policies!

            var result = policyWrap.ExecuteAsync(async ()
                => await openWeatherClient.CurrentWeather.GetByName(city, OpenWeatherMap.MetricSystem.Metric)).Result;
            var weather = WeatherConverter.GetWeatherFromResponse(result);

            return weather;
        }


        private Task<OpenWeatherMap.CurrentWeatherResponse> DoFallbackAction(CancellationToken ct, string city)
        {
            // TODO get weather from a real cache
            OpenWeatherMap.CurrentWeatherResponse response = new OpenWeatherMap.CurrentWeatherResponse()
            {
                Weather = new OpenWeatherMap.Weather() { Value = "cloudy" },
                Temperature = new OpenWeatherMap.Temperature() { Value = 11.11, Unit = "celcius" },
                City = new OpenWeatherMap.City() { Name = city + " (fallback)" },
            };

            var resultTask = new Task<OpenWeatherMap.CurrentWeatherResponse>(() => response);
            resultTask.Start();

            return resultTask;
        }

        private AsyncInjectLatencyPolicy GetSimmyLatencyPolicy()
        {
            AsyncInjectLatencyPolicy chaosPolicy = MonkeyPolicy.InjectLatencyAsync(
                latency: TimeSpan.FromSeconds(5),
                injectionRate: 1, // 100% injectionRate
                enabled: () => IsChaosPolicyEnabled()
                );

            return chaosPolicy;
        }

        private bool IsFallbackPolicyEnabled(Exception ex)
        {
            if (ex is OpenWeatherMap.OpenWeatherMapException owme)
            {
                if (owme.StatusCode == System.Net.HttpStatusCode.NotFound) // requested city was not found
                    return false;
            }
            return true;
        }

        private bool IsChaosPolicyEnabled()
        {
            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            return System.IO.File.Exists(Path.Combine(assemblyPath, SharedData.ChaosEnabledFileName));
        }
    }
}
