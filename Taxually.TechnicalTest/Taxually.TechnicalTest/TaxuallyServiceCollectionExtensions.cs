using Taxually.TechnicalTest.BusinessLogic.CompanyRegistrationHandlers;
using Taxually.TechnicalTest.Infrastructure.TaxuallyHttpClient;
using Taxually.TechnicalTest.Infrastructure.TaxuallyQueueClient;
using Taxually.TechnicalTest.Mappers;
using Taxually.TechnicalTest.Models.Settings.VatRegistration;
using Taxually.TechnicalTest.Utilities.CsvHelperWrapper;
using Taxually.TechnicalTest.Utilities.XmlSerializerWrapper;

namespace Taxually.TechnicalTest
{
    public static class TaxuallyServiceCollectionExtensions
    {
        public static IServiceCollection AddTaxuallyServices(this IServiceCollection services)
        {
            services.AddSingleton<ICompanyRegistrationHandler, GBCompanyRegistrationHandler>();
            services.AddSingleton<ICompanyRegistrationHandler, FRCompanyRegistrationHandler>();
            services.AddSingleton<ICompanyRegistrationHandler, DECompanyRegistrationHandler>();

            services.AddSingleton<ICsvHelperWrapper, CsvHelperWrapper>();
            services.AddSingleton<IXmlSerializerWrapper, XmlSerializerWrapper>();

            services.AddSingleton<ITaxuallyHttpClient, TaxuallyHttpClient>();
            services.AddSingleton<ITaxuallyQueueClient, TaxuallyQueueClient>();

            services.AddSingleton<IVatRegistrationMapper, VatRegistrationMapper>();

            return services;
        }

        public static IServiceCollection AddTaxuallySettings(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddOptions<VatRegistrationSettings>()
                    .Bind(configurationManager.GetSection(VatRegistrationSettings.VatRegistration))
                    .ValidateDataAnnotations()
                    .ValidateOnStart(); 

            services.AddOptions<GBSettings>()
                    .Bind(configurationManager.GetSection($"{VatRegistrationSettings.VatRegistration}:{GBSettings.GB}"))
                    .ValidateDataAnnotations()
                    .ValidateOnStart();

            services.AddOptions<FRSettings>()
                    .Bind(configurationManager.GetSection($"{VatRegistrationSettings.VatRegistration}:{FRSettings.FR}"))
                    .ValidateDataAnnotations()
                    .ValidateOnStart();

            services.AddOptions<DESettings>()
                    .Bind(configurationManager.GetSection($"{VatRegistrationSettings.VatRegistration}:{DESettings.DE}"))
                    .ValidateDataAnnotations()
                    .ValidateOnStart();

            return services;
        }
    }
}
