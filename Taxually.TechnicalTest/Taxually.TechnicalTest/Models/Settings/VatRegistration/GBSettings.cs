using System.ComponentModel.DataAnnotations;

namespace Taxually.TechnicalTest.Models.Settings.VatRegistration
{
    public class GBSettings
    {
        public const string GB = "GB";

        [Required]
        [Url]
        public string URL { get; init; } = null!;
    }
}
