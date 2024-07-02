using Microsoft.Extensions.Options;
using Taxually.TechnicalTest.BusinessLogic.Enums;
using Taxually.TechnicalTest.BusinessLogic.Models;
using Taxually.TechnicalTest.Infrastructure.TaxuallyHttpClient;
using Taxually.TechnicalTest.Models.Settings.VatRegistration;

namespace Taxually.TechnicalTest.BusinessLogic.CompanyRegistrationHandlers
{
    public class GBCompanyRegistrationHandler : CompanyRegistrationHandlerBase
    {
        private ITaxuallyHttpClient _taxuallyHttpClient;
        private GBSettings _GBSettings;

        public GBCompanyRegistrationHandler(ITaxuallyHttpClient taxuallyHttpClient, IOptions<GBSettings> GBOptions) : base(CountryCode.GB)
        {
            _taxuallyHttpClient = taxuallyHttpClient;
            _GBSettings = GBOptions.Value;
        }

        public async override Task HandleRegistration(VatRegistration vatRegistration)
        {
            await _taxuallyHttpClient.PostAsync(_GBSettings.URL, vatRegistration);
        }
    }
}
