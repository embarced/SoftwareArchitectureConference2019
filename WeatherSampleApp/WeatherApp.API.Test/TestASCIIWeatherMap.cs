using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherApp.API.BusinessLogic;

namespace WeatherApp.API.Test
{

    [TestClass]
    public class TestASCIIWeatherMap
    {
        private TestContext testContextInstance;

        /// <summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestMethod]
        public void TestPrintWeatherASCII()
        {
            List<int> weatherIds = new List<int>() { 200, 300, 500, 600, 801, 800, 10000 };

            foreach (int id in weatherIds)
            {
                string ascii = ASCIIWeatherMap.GetASCIIFromOpenWeatherResponse(id);
                TestContext.WriteLine(ascii + Environment.NewLine);
            }
        }
    }
}
