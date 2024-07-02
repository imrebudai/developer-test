using Taxually.TechnicalTest.BusinessLogic.Models;
using Taxually.TechnicalTest.Models.Api.VatRegistration;

namespace Taxually.TechnicalTest.Mappers
{
    public interface IVatRegistrationMapper
    {
        public VatRegistration ToBusinessDto(VatRegistrationRequest vatRegistrationRequest);
    }
}
