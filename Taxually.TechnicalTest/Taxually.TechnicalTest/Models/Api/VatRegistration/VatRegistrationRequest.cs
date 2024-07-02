using System.ComponentModel.DataAnnotations;

namespace Taxually.TechnicalTest.Models.Api.VatRegistration
{
    public record VatRegistrationRequest([Required] string CompanyName, [Required] string CompanyId, [Required] string Country);
}
