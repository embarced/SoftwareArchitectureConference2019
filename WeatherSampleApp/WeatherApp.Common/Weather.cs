using System;
using System.Collections.Generic;
using System.Linq;

namespace WeatherApp.Common
{
    public class Weather
    {
        public string City { get; set; }

        public Temperature Temperature { get; set; }

        public string Value { get; set; }

        public ASCIIWeatherRepresentation ASCIIWeatherRepresentation { get; set;  }

        public override string ToString()
        {
            return string.Format("{0}; Temperature: {1}, Weather: {2}{3}{4}", City, Temperature, Value, Environment.NewLine, ASCIIWeatherRepresentation);
        }
    }
}
