using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Kloud_CarsInfoProcessor.Test
{
    [TestClass]
    public class CarsController_Test
    {
        private readonly HttpClient _client;
        private readonly string _url;
        public CarsController_Test()
        {
            _client = TestUtils.CreateHttpClient();
            _url = "api/cars";
        }
        /// <summary>
        /// Test to get full list of owners and car details from json
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetCarsJsonDataTest()
        {
            var response = await _client.GetAsync($"{_url}");
            var dataAsString = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK, "Failed to obtain json data from API");
            Assert.IsTrue(!string.IsNullOrEmpty(dataAsString), "Failed to obtain json data from API");
        }

        /// <summary>
        /// Test to get car details based on owner name
        /// </summary>
        /// <returns></returns>
        [DataTestMethod]
        [DataRow("Brooke", HttpStatusCode.OK)]
        [DataRow("matilda", HttpStatusCode.OK)]
        [DataRow("KRISTIN", HttpStatusCode.OK)]
        [DataRow("", HttpStatusCode.OK)]
        [DataRow("Ram", HttpStatusCode.NotFound)]
        [DataRow("1234", HttpStatusCode.NotFound)]
        [DataRow(null, HttpStatusCode.OK)]
        public async Task GetCarsJsonDataByOwnerTest(string ownername, HttpStatusCode statuscode)
        {
            var response = await _client.GetAsync($"{_url}/GetJsonByOwner?ownername={ownername}");
            var dataAsString = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(response.StatusCode == statuscode, "Failed to obtain json data from API");
        }

        /// <summary>
        /// Test Get Owner names based on car colour
        /// </summary>
        /// <returns></returns>
        [DataTestMethod]
        [DataRow("Blue", HttpStatusCode.OK)]
        [DataRow("red", HttpStatusCode.OK)]
        [DataRow("GREEN", HttpStatusCode.OK)]
        [DataRow("", HttpStatusCode.OK)]
        [DataRow("Pink", HttpStatusCode.NotFound)]
        [DataRow("1234", HttpStatusCode.NotFound)]
        [DataRow(null, HttpStatusCode.OK)]
        public async Task GetOwnerDataByCarColourTest(string colour, HttpStatusCode statuscode)
        {
            var response = await _client.GetAsync($"{_url}/GetOwnersByColour?colour={colour}");
            var dataAsString = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(response.StatusCode == statuscode, "Failed to obtain json data from API");
        }


        /// <summary>
        /// Test Get Owner names based on car brand
        /// </summary>
        /// <returns></returns>
        [DataTestMethod]
        [DataRow("Holden", HttpStatusCode.OK)]
        [DataRow("bmw", HttpStatusCode.OK)]
        [DataRow("MG", HttpStatusCode.OK)]
        [DataRow("", HttpStatusCode.NotFound)]
        [DataRow("Hyundai", HttpStatusCode.NotFound)]
        [DataRow("1234", HttpStatusCode.NotFound)]
        [DataRow(null, HttpStatusCode.NotFound)]
        public async Task GetOwnerDataByCarBrandTest(string brand, HttpStatusCode statuscode)
        {
            var response = await _client.GetAsync($"{_url}/GetOwnersByBrand?brand={brand}");
            var dataAsString = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(response.StatusCode == statuscode, "Failed to obtain json data from API");
        }
    }
}
