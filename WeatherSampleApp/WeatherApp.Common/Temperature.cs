using System;
namespace WeatherApp.Common
{
    public class Temperature
    {
        public TemperatureKind TemperatureKind { get; set; }
        public double Value { get; set; }

        public Temperature() { }

        public Temperature(double value) : this(value, TemperatureKind.Celsius)
        {
        }

        public Temperature(double value, TemperatureKind temperatureKind)
        {
            TemperatureKind = temperatureKind;
            Value = value;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Value, TemperatureKind);
        }
    }

    public enum TemperatureKind
    {
        Celsius,
        Fahrendheit
    }


}
