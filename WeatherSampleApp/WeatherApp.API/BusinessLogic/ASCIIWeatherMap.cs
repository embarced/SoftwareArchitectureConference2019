using System;
using System.Collections.Generic;

namespace WeatherApp.API.BusinessLogic
{
    public static class ASCIIWeatherMap
    {
        private static Dictionary<int, string> asciiArtDictionary = new Dictionary<int, string>()
        {
            { 2, // Code: 200-232, thunderstorm
@"  .-.
 (   ).
(___(__)
   /_ /
    // 
    /"},

            { 3, //Code: 300-321, drizzle
@"  .-.    
 (   ).  
(___(__) 
  ‘   ‘ 
 ‘   ‘  "},

            { 5, // Code: 500-531, (light) rain
@"     .--.    
  .-(    ).  
 (___.__)__) 
  ‘ ‘ ‘ ‘ ‘  
 ‘ ‘ ‘ ‘ ‘ "},

            { 6, // Code: 600-622, snow
@"      .--.    
  .-(     ).  
 (___.__)__) 
  * * * * *
 * * * * * "},

            { 8, // Code: 801-804, cloudy
@"     .--.    
  .-(    ).  
 (___.__)__) "},
            { 0, // Code: 800, clear sky
@"
    \   /  
     .-.   
 - (   ) -
     '-'   
    /   \  "},
        };

        public static string GetASCIIFromOpenWeatherResponse(OpenWeatherMap.CurrentWeatherResponse response)
        {
            int key = response.Weather.Number;

            return GetASCIIFromOpenWeatherResponse(key);
        }
        public static string GetASCIIFromOpenWeatherResponse(int key)
        {

            int internalKey = MapKey(key);

            if (internalKey == -1) // FallBack
                return GetDefaultWeather();
            else
            {
                string weather = asciiArtDictionary.GetValueOrDefault(internalKey);
                return weather;
            }
        }

        private static int MapKey(int key)
        {
            switch (key)
            {
                case int n when (n >= 200 && n <= 232):
                    return 2;
                case int n when (n >= 300 && n <= 321):
                    return 3;
                case int n when (n >= 500 && n <= 531):
                    return 5;
                case int n when (n >= 600 && n <= 622):
                    return 6;
                case int n when (n >= 801 && n <= 804):
                    return 8;
                case 800:
                    return 0;
                default:
                    return -1; // Unknow Weather!
            }
        }

        private static string GetDefaultWeather()
        {
            return
@"      .--.    
  .-(????).  
 (___.__)__)";
        }
    }
}
