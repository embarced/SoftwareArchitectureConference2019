using System.IO;
using WeatherApp.Client.Common;
using WeatherApp.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Basta2019_Weather.Client.Test
{
    [TestClass]
    public class ClientIntegrationTestWithChaos
    {
        string uri = "https://localhost:5001/";

        string filePathToChaosSettings = "/Users/renew/Projects/Basta2019/Basta2019_Weather/Basta2019_WeatherAPI/bin/Debug/netcoreapp2.1";

        [TestInitialize]
        public void Setup()
        {
            // Enable Polly on target test environment by writing test-file
            File.WriteAllText(Path.Combine(filePathToChaosSettings, SharedData.ChaosEnabledFileName), "chaosTestingEnabled:true");
        }

        [TestCleanup]
        public void Teardown()
        {
            // Disable Polly on target test environment by writing test-file
            File.Delete(Path.Combine(filePathToChaosSettings, SharedData.ChaosEnabledFileName));
        }

        [TestMethod]
        public void TestGetWeatherLondon()
        {
            using (WeatherClient client = new WeatherClient(uri))
            {
                string city = "London";
                Weather result = client.GetWeatherResilientAsync(city).Result;

                StringAssert.Contains(result.City, city);
                StringAssert.Contains(result.City, "fallback"); // in case of fallback -> is added to city name
            }
        }
    }
}
