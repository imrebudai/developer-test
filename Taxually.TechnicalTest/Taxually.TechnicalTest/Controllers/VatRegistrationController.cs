using Microsoft.AspNetCore.Mvc;
using Taxually.TechnicalTest.BusinessLogic.CompanyRegistrationHandlers;
using Taxually.TechnicalTest.BusinessLogic.Models;
using Taxually.TechnicalTest.Constants;
using Taxually.TechnicalTest.Mappers;
using Taxually.TechnicalTest.Models.Api.VatRegistration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Taxually.TechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatRegistrationController : ControllerBase
    {
        private IEnumerable<ICompanyRegistrationHandler> _companyRegistrationHandlers;
        private IVatRegistrationMapper _vatRegistrationMapper;

        public VatRegistrationController(IEnumerable<ICompanyRegistrationHandler> companyRegistrationHandlers, IVatRegistrationMapper vatRegistrationMapper)
        {
            _companyRegistrationHandlers = companyRegistrationHandlers;
            _vatRegistrationMapper = vatRegistrationMapper;
        }

        /// <summary>
        /// Registers a company for a VAT number in a given country
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] VatRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ICompanyRegistrationHandler? companyRegistrationHandler = _companyRegistrationHandlers.SingleOrDefault(handler => handler.IsCountryCodeSupported(request.Country));

            if(companyRegistrationHandler == null)
            {
                return BadRequest(ErrorMessageConstants.CountryNotSupported);
            }

            VatRegistration vatRegistrationBusinessDto = _vatRegistrationMapper.ToBusinessDto(request);
            await companyRegistrationHandler.HandleRegistration(vatRegistrationBusinessDto);

            return Ok();         
        }
    }
}
