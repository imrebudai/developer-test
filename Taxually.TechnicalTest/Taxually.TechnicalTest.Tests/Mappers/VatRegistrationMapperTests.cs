using Taxually.TechnicalTest.BusinessLogic.Models;
using Taxually.TechnicalTest.Mappers;
using Taxually.TechnicalTest.Models.Api.VatRegistration;

namespace Taxually.TechnicalTest.Tests.Mappers
{
    public class VatRegistrationMapperTests
    {
        private readonly VatRegistrationMapper _vatRegistrationMapper;

        public VatRegistrationMapperTests()
        {
            _vatRegistrationMapper = new VatRegistrationMapper();
        }

        [Fact]
        public void ToBusinessDto_ShouldReturnTheCorrectMapping()
        {
            // Arrange
            string companyName = "abc company";
            string companyId = "abc";
            string country = "GB";

            VatRegistrationRequest vatRegistrationRequest = new(companyName, companyId, country);

            // Act
            VatRegistration result = _vatRegistrationMapper.ToBusinessDto(vatRegistrationRequest);

            // Assert
            Assert.Equal(vatRegistrationRequest.CompanyName, result.CompanyName);
            Assert.Equal(vatRegistrationRequest.CompanyId, result.CompanyId);
            Assert.Equal(vatRegistrationRequest.Country, result.Country);
        }
    }
}
