using System;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using WeatherApp.Common;
using Newtonsoft.Json;

namespace WeatherApp.Client.Common
{
    public class WeatherClient : IDisposable
    {
        private System.Net.Http.HttpClient client;

        private string apiAdressWeather = "api/weather/";
        private string apiAdressWeatherResilient = "api/weatherresilient/";

        public WeatherClient(string uri)
        {
            client = new System.Net.Http.HttpClient();
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET /weather/city
        public async Task<Weather> GetWeatherAsync(string city)
        {
            return await GetWeatherInternalAsync(apiAdressWeather, city);
        }

        // GET /weatherresilient/city
        public async Task<Weather> GetWeatherResilientAsync(string city)
        {
            return await GetWeatherInternalAsync(apiAdressWeatherResilient, city);
        }

        private async Task<Weather> GetWeatherInternalAsync(string apiAdress, string city)
        {
            var resp2 = await client.GetAsync(apiAdress + city);

            if (resp2.IsSuccessStatusCode)
            {
                string content = await resp2.Content.ReadAsStringAsync();

                return GetWeatherFromJson(content);
            }
            else
            {
                throw new InvalidOperationException(string.Format("Invalid Request (Status Code: {0})", resp2.StatusCode)); // TODO add response message to exception
            }
        }

        private Weather GetWeatherFromJson(string json)
        {
            var weather = JsonConvert.DeserializeObject<Weather>(json);
            return weather;
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
