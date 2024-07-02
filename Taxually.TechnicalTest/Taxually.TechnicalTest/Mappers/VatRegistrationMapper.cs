using Taxually.TechnicalTest.BusinessLogic.Models;
using Taxually.TechnicalTest.Models.Api.VatRegistration;

namespace Taxually.TechnicalTest.Mappers
{
    public class VatRegistrationMapper : IVatRegistrationMapper
    {
        public VatRegistration ToBusinessDto(VatRegistrationRequest vatRegistrationRequest)
        {
            return new VatRegistration(vatRegistrationRequest.CompanyName, vatRegistrationRequest.CompanyId, vatRegistrationRequest.Country);
        }
    }
}
