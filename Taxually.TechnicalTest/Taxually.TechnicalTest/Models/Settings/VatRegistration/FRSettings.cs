using System.ComponentModel.DataAnnotations;

namespace Taxually.TechnicalTest.Models.Settings.VatRegistration
{
    public class FRSettings
    {
        public const string FR = "FR";

        [Required]
        public string VatRegistrationCsvFileName { get; init; } = null!;
    }
}
