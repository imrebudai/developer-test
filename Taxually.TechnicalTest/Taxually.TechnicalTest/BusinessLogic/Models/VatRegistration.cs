namespace Taxually.TechnicalTest.BusinessLogic.Models
{
    public record VatRegistration
    {
        public string CompanyName { get; init; } = string.Empty;
        public string CompanyId { get; init; } = string.Empty;
        public string Country { get; init; } = string.Empty;

        public VatRegistration()
        {
            
        }

        public VatRegistration(string companyName, string companyId, string country)
        {
            CompanyName = companyName;
            CompanyId = companyId;
            Country = country;
        }
    }
}
