using Microsoft.Extensions.Options;
using System.Globalization;
using System.Text;
using Taxually.TechnicalTest.BusinessLogic.Enums;
using Taxually.TechnicalTest.BusinessLogic.Models;
using Taxually.TechnicalTest.Infrastructure.TaxuallyQueueClient;
using Taxually.TechnicalTest.Models.Settings.VatRegistration;
using Taxually.TechnicalTest.Utilities.CsvHelperWrapper;

namespace Taxually.TechnicalTest.BusinessLogic.CompanyRegistrationHandlers
{
    public class FRCompanyRegistrationHandler : CompanyRegistrationHandlerBase
    {
        private ICsvHelperWrapper _csvHelperWrapper;
        private ITaxuallyQueueClient _taxuallyQueueClient;
        private FRSettings _FRSettings;

        public FRCompanyRegistrationHandler(ICsvHelperWrapper csvHelperWrapper, ITaxuallyQueueClient taxuallyQueueClient,
             IOptions<FRSettings> FROptions) : base(CountryCode.FR)
        {
            _csvHelperWrapper = csvHelperWrapper;
            _taxuallyQueueClient = taxuallyQueueClient;
            _FRSettings = FROptions.Value;
        }

        public async override Task HandleRegistration(VatRegistration vatRegistration)
        {
            List<VatRegistration> csvRecords = new() { vatRegistration };
            string csv = _csvHelperWrapper.GenerateCsv(csvRecords, CultureInfo.InvariantCulture);
            byte[] csvBytes = Encoding.UTF8.GetBytes(csv);

            await _taxuallyQueueClient.EnqueueAsync(_FRSettings.VatRegistrationCsvFileName, csvBytes);
        }
    }
}
