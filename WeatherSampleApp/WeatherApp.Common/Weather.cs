using System;
using System.Collections.Generic;

namespace WeatherApp.Common
{
    public class Weather
    {
        public string City { get; set; }

        public Temperature Temperature { get; set; }

        public string Value { get; set; }

        public List<WeatherRepresentation> WeatherRepresentations { get; set; } // TODO: make immutable... ;)

        public override string ToString()
        {
            return string.Format("{0}; Temperature: {1}, Weather: {2}", City, Temperature, Value);
        }
    }
}
