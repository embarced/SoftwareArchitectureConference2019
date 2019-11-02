using System;
namespace WeatherApp.Common
{
    public class ASCIIWeatherRepresentation
    {
        public string WeatherRepresentation { get; set; }

        public ASCIIWeatherRepresentation(string asciiString)
        {
            WeatherRepresentation = asciiString;
        }

        public override string ToString()
        {
            return WeatherRepresentation;
        }
    }
}
