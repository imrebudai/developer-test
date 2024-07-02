using Taxually.TechnicalTest.BusinessLogic.Enums;
using Taxually.TechnicalTest.BusinessLogic.Models;

namespace Taxually.TechnicalTest.BusinessLogic.CompanyRegistrationHandlers
{
    public abstract class CompanyRegistrationHandlerBase : ICompanyRegistrationHandler
    {
        protected CountryCode _supportedCountryCode;

        public CompanyRegistrationHandlerBase(CountryCode supportedCountryCode)
        {
            _supportedCountryCode = supportedCountryCode;
        }

        public abstract Task HandleRegistration(VatRegistration vatRegistration);

        public bool IsCountryCodeSupported(string countryCode)
        {
            return countryCode.Equals(_supportedCountryCode.ToString(), StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
