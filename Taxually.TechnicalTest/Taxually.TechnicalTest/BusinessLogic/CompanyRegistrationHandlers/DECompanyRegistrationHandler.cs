using Microsoft.Extensions.Options;
using Taxually.TechnicalTest.BusinessLogic.Enums;
using Taxually.TechnicalTest.BusinessLogic.Models;
using Taxually.TechnicalTest.Infrastructure.TaxuallyQueueClient;
using Taxually.TechnicalTest.Models.Settings.VatRegistration;
using Taxually.TechnicalTest.Utilities.XmlSerializerWrapper;

namespace Taxually.TechnicalTest.BusinessLogic.CompanyRegistrationHandlers
{
    public class DECompanyRegistrationHandler : CompanyRegistrationHandlerBase
    {
        private IXmlSerializerWrapper _xmlSerializerWrapper;
        private ITaxuallyQueueClient _taxuallyQueueClient;
        private DESettings _DESettings;

        public DECompanyRegistrationHandler(IXmlSerializerWrapper xmlSerializerWrapper,
            ITaxuallyQueueClient taxuallyQueueClient, IOptions<DESettings> DEOptions) : base(CountryCode.DE)
        {
            _xmlSerializerWrapper = xmlSerializerWrapper;
            _taxuallyQueueClient = taxuallyQueueClient;
            _DESettings = DEOptions.Value;
        }

        public async override Task HandleRegistration(VatRegistration vatRegistration)
        {
            string vatRegistrationXml = _xmlSerializerWrapper.Serialize(vatRegistration);

            await _taxuallyQueueClient.EnqueueAsync(_DESettings.VatRegistrationXmlFileName, vatRegistrationXml);
        }
    }
}
