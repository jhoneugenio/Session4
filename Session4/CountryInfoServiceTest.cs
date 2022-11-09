using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Session4
{
    [TestClass]
    public class CountryInfoServiceTests
    {
        public readonly ServiceReference.CountryInfoServiceSoapTypeClient countryDetails =
            new ServiceReference.CountryInfoServiceSoapTypeClient(ServiceReference.CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);

        [TestMethod]
        public void ValidateCountryNamesByCode()
        {
            var countryNamesByCode = countryDetails.ListOfCountryNamesByCode();
            var countryNamesResult = countryNamesByCode.OrderBy(a => a.sISOCode);
            var isAscending = countryNamesByCode.SequenceEqual(countryNamesResult);
            Assert.IsTrue(isAscending);
        }

        [TestMethod]
        public void ValidateInvalidCountryCode()
        {
            var invalidCountryCode = "LOK";
            var responseMessage = countryDetails.CountryName(invalidCountryCode);
            Assert.IsTrue(responseMessage.Contains("Country not found in the database"), $"Country code {invalidCountryCode} displayed in DB");
        }

        [TestMethod]
        public void ValidateLastCountryName()
        {
            var countryDetailsList = countryDetails.ListOfCountryNamesByCode();
            var lastCount = countryDetailsList.Last();
            var country = countryDetails.CountryName(lastCount.sISOCode);
            Assert.AreEqual(lastCount.sName, country);
        }
    }
}