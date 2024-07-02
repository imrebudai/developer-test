namespace Taxually.TechnicalTest.Infrastructure.TaxuallyHttpClient
{
    public interface ITaxuallyHttpClient
    {
        public Task PostAsync<TRequest>(string url, TRequest request);
    }
}
