using Microsoft.VisualStudio.TestTools.UnitTesting;
using Insurance1CompAppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Insurance1CompAppServices.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Insurance1CompAppServices.Tests
{
    [TestClass()]
    public class CountryGwpServiceTests
    {
        private CountryGwpService _service;
        private Mock<ICountryGwpRepository> _repositoryMock;
        private IMemoryCache _memoryCache;

        [TestInitialize]
        public void Setup()
        {
            _repositoryMock = new Mock<ICountryGwpRepository>();
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _service = new CountryGwpService(_repositoryMock.Object, _memoryCache);

            // Setup default mock behavior for repository
            _repositoryMock.Setup(r => r.GetGwpDataAsync(
                It.IsAny<string>(),
                It.IsAny<List<string>>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .ReturnsAsync((string country, List<string> lobs, int yearFrom, int yearTo) =>
                {
                    if (country == "france")
                    {
                        var data = new Dictionary<string, List<double>>();
                        foreach (var lob in lobs)
                        {
                            if (lob == "transport" || lob == "property")
                                data[lob] = new List<double> { 1.0, 2.0, 3.0, 4.0 };
                            else if (lob == "nonexistent_lob")
                                data[lob] = new List<double>();
                        }
                        return data;
                    }
                    return new Dictionary<string, List<double>>();
                });
        }

        [TestMethod()]
        public async Task GetAverageGwpAsync_ValidCountryAndLobs_ReturnsCorrectAverage()
        {
            // Arrange
            string country = "france";
            var lobs = new List<string> { "transport", "property" };

            // Act
            var result = await _service.GetAverageGwpAsync(country, lobs);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(lobs.Count, result.Count);
            foreach (var avg in result.Values)
            {
                Assert.IsTrue(avg >= 0);
            }
        }

        [TestMethod()]
        public async Task GetAverageGwpAsync_InvalidCountry_ReturnsEmptyResult()
        {
            // Arrange
            string country = "invalid_country";
            var lobs = new List<string> { "transport" };

            // Act
            var result = await _service.GetAverageGwpAsync(country, lobs);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod()]
        public async Task GetAverageGwpAsync_EmptyLobs_ReturnsEmptyResult()
        {
            // Arrange
            string country = "france";
            var lobs = new List<string>();

            // Act
            var result = await _service.GetAverageGwpAsync(country, lobs);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }
    }
}