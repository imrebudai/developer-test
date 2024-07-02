using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Taxually.TechnicalTest.BusinessLogic.CompanyRegistrationHandlers;
using Taxually.TechnicalTest.BusinessLogic.Models;
using Taxually.TechnicalTest.Constants;
using Taxually.TechnicalTest.Controllers;
using Taxually.TechnicalTest.Mappers;
using Taxually.TechnicalTest.Models.Api.VatRegistration;

namespace Taxually.TechnicalTest.Tests.Controllers
{
    public class VatRegistrationControllerTests
    {
        const string DECountryCode = "DE";
        private readonly VatRegistrationController _vatRegistrationController;
        private List<ICompanyRegistrationHandler> _companyRegistrationHandlers;
        private IVatRegistrationMapper _vatRegistrationMapper;

        public VatRegistrationControllerTests()
        {
            _vatRegistrationMapper = Substitute.For<IVatRegistrationMapper>();
            _companyRegistrationHandlers = new List<ICompanyRegistrationHandler>() {};

            _vatRegistrationController = new VatRegistrationController(_companyRegistrationHandlers, _vatRegistrationMapper);
        }

        [Fact]
        public async Task Post_ShouldReturnBadRequest_WhenTheModelStateIsInvalid()
        {
            // Arrange
            _vatRegistrationController.ModelState.AddModelError("test", "test");
            VatRegistrationRequest vatRegistrationRequest = new VatRegistrationRequest("", "", "");

            // Act
            ActionResult result = await _vatRegistrationController.Post(vatRegistrationRequest);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Post_ShouldReturnBadRequestWithCountryNotSupportedMessage_WhenCompanyRegistrationHandlerIsNotFound()
        {
            // Arrange
            VatRegistrationRequest vatRegistrationRequest = new VatRegistrationRequest("abc", "a", "HU");

            // Act
            ActionResult result = await _vatRegistrationController.Post(vatRegistrationRequest);

            // Assert
            BadRequestObjectResult badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(ErrorMessageConstants.CountryNotSupported, badRequestResult.Value);
        }

        [Fact]
        public async Task Post_ShouldThrowException_WhenCompanyMoreThanOneCompanyRegistrationHandlerIsFound()
        {
            // Arrange            
            ICompanyRegistrationHandler companyRegistrationHandler = Substitute.For<ICompanyRegistrationHandler>();
            ICompanyRegistrationHandler companyRegistrationHandler2 = Substitute.For<ICompanyRegistrationHandler>();

            companyRegistrationHandler.IsCountryCodeSupported(DECountryCode).Returns(true);
            companyRegistrationHandler2.IsCountryCodeSupported(DECountryCode).Returns(true);

            _companyRegistrationHandlers.Add(companyRegistrationHandler);
            _companyRegistrationHandlers.Add(companyRegistrationHandler2);

            VatRegistrationRequest vatRegistrationRequest = new VatRegistrationRequest("abc", "a", DECountryCode);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _vatRegistrationController.Post(vatRegistrationRequest));
        }

        [Fact]
        public async Task Post_ShouldCallHandleRegistrationOnTheProperCompanyRegistrationHandlerWithTheExpectedParameter()
        {
            // Arrange
            ICompanyRegistrationHandler DECompanyRegistrationHandler = Substitute.For<ICompanyRegistrationHandler>();

            DECompanyRegistrationHandler.IsCountryCodeSupported(DECountryCode).Returns(true);

            _companyRegistrationHandlers.Add(DECompanyRegistrationHandler);

            VatRegistrationRequest vatRegistrationRequest = new VatRegistrationRequest("abc", "a", DECountryCode);

            VatRegistration vatRegistrationBusinessDto = new VatRegistration(vatRegistrationRequest.CompanyName, vatRegistrationRequest.CompanyId, vatRegistrationRequest.Country);

            _vatRegistrationMapper.ToBusinessDto(vatRegistrationRequest)
                                  .Returns(vatRegistrationBusinessDto);

            // Act
            _ = await _vatRegistrationController.Post(vatRegistrationRequest);

            // Assert
            await DECompanyRegistrationHandler.Received().HandleRegistration(vatRegistrationBusinessDto);
        }
    }
}
