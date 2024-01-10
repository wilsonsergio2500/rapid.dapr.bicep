using Dapr.Client;

namespace Rapid.Dapr.Push.Services
{
    public interface IDaprClientFactory
    {
        Task<bool> PublishToDapr<T>(string pubsubName, string topicName, T payload);
    }
    public class DaprClientFactory : IDaprClientFactory
    {
        public async Task<bool> PublishToDapr<T>(string pubsubName, string topicName, T payload)
        {
            DaprClient client = new DaprClientBuilder().Build();
            await client.PublishEventAsync<T>(pubsubName, topicName, payload);
            return true;
        }
    }
}
