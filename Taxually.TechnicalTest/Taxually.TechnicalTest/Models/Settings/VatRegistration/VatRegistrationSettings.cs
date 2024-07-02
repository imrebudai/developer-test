using System.ComponentModel.DataAnnotations;

namespace Taxually.TechnicalTest.Models.Settings.VatRegistration
{
    public class VatRegistrationSettings
    {
        public const string VatRegistration = "VatRegistration";

        [Required]
        public GBSettings GB { get; init; } = null!;

        [Required]
        public FRSettings FR { get; init; } = null!;

        [Required]
        public DESettings DE { get; init; } = null!;
    }
}
