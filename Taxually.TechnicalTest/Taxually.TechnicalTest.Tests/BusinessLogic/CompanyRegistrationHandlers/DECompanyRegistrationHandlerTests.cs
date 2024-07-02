using Microsoft.Extensions.Options;
using NSubstitute;
using Taxually.TechnicalTest.BusinessLogic.CompanyRegistrationHandlers;
using Taxually.TechnicalTest.BusinessLogic.Models;
using Taxually.TechnicalTest.Infrastructure.TaxuallyQueueClient;
using Taxually.TechnicalTest.Models.Settings.VatRegistration;
using Taxually.TechnicalTest.Utilities.XmlSerializerWrapper;

namespace Taxually.TechnicalTest.Tests.BusinessLogic.CompanyRegistrationHandlers
{
    public class DECompanyRegistrationHandlerTests
    {
        private readonly DECompanyRegistrationHandler _DECompanyRegistrationHandler;
        private readonly IXmlSerializerWrapper _xmlSerializerWrapper;
        private readonly ITaxuallyQueueClient _taxuallyQueueClient;
        private readonly IOptions<DESettings> _DEOptions;
        private readonly DESettings _DESettings;
        private readonly VatRegistration _vatRegistration;

        public DECompanyRegistrationHandlerTests()
        {
            _xmlSerializerWrapper = Substitute.For<IXmlSerializerWrapper>();
            _taxuallyQueueClient = Substitute.For<ITaxuallyQueueClient>();
            _DEOptions = Substitute.For<IOptions<DESettings>>();

            _DESettings = new DESettings() { VatRegistrationXmlFileName = "test_xml" };

            string companyName = "abc company";
            string companyId = "abc";
            string country = "GB";

            _vatRegistration = new(companyName, companyId, country);

            _DEOptions.Value.Returns(_DESettings);

            _DECompanyRegistrationHandler = new DECompanyRegistrationHandler(_xmlSerializerWrapper, _taxuallyQueueClient, _DEOptions);
        }

        [Theory]
        [InlineData(true, "DE")]
        [InlineData(false, "GB")]
        public void IsCountryCodeSupported_ShouldReturnExpectedResult_WhenCalledWithCountryCode(bool expectedResult, string countryCode)
        {
            // Arrange

            // Act
            var result = _DECompanyRegistrationHandler.IsCountryCodeSupported(countryCode);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task HandleRegistration_ShouldCallSerializeOnIXmlSerializerWrapperWithTheExpectedParameter()
        {
            // Arrange

            // Act
            await _DECompanyRegistrationHandler.HandleRegistration(_vatRegistration);

            // Assert
            _xmlSerializerWrapper.Received().Serialize(_vatRegistration);
        }

        [Fact]
        public async Task HandleRegistration_ShouldCallEnqueueAsyncOnITaxuallyQueueClientWithTheExpectedParameters()
        {
            // Arrange
            string testXml = "testXml";
            _xmlSerializerWrapper.Serialize(_vatRegistration).Returns(testXml);

            // Act
            await _DECompanyRegistrationHandler.HandleRegistration(_vatRegistration);

            // Assert
            await _taxuallyQueueClient.Received().EnqueueAsync(_DESettings.VatRegistrationXmlFileName, testXml);
        }      
    }
}
