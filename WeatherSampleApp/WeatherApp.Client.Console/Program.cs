using System;
using WeatherApp.Client.Common;

namespace WeatherApp.Client.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Enter city name to retrieve weather [Enter]! (just hit [Enter] to close)");

            string uri = "https://localhost:5001/";

            string inputCity;

            while (!string.IsNullOrEmpty(inputCity = System.Console.ReadLine()))
            { 
                using (WeatherClient client = new WeatherClient(uri))
                {
                    var weatherResultTask = client.GetWeatherAsync(inputCity);
                    // use resilient version of API
                    //var weatherResultTask = client.GetWeatherResilientAsync(inputCity);
                    var weather = weatherResultTask.Result;
                    System.Console.WriteLine(weather);
                }
            }
        }
    }
}
