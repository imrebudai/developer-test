using System.ComponentModel.DataAnnotations;

namespace Taxually.TechnicalTest.Models.Settings.VatRegistration
{
    public class DESettings
    {
        public const string DE = "DE";

        [Required]
        public string VatRegistrationXmlFileName { get; init; } = null!;
    }
}
