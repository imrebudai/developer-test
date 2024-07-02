namespace Taxually.TechnicalTest.Infrastructure.TaxuallyQueueClient
{
    public interface ITaxuallyQueueClient
    {
        public Task EnqueueAsync<TPayload>(string queueName, TPayload payload);
    }
}
