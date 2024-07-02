using Taxually.TechnicalTest.BusinessLogic.Models;

namespace Taxually.TechnicalTest.BusinessLogic.CompanyRegistrationHandlers
{
    public interface ICompanyRegistrationHandler
    {
        public bool IsCountryCodeSupported(string countryCode); 
        public Task HandleRegistration(VatRegistration vatRegistration);
    }
}
