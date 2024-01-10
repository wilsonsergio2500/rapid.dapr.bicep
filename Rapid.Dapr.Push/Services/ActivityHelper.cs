
using Rapid.Dapr.Task.Core.DTOs;
using Rapid.Dapr.Task.Core.Helpers;

namespace Rapid.Dapr.Push.Services
{

    public interface IActivityHelper
    {
        Task<bool> PublishActivity(ActivityMessageDTO activityMessage);
        Task<bool> PublishActivityOfValueType<T>(ActivityDTO<T> activity);
        Task<bool> PublishToTopic<T>(string topic, T value);
        Task<bool> PublishToTopic<T>(string topic, string value);
    }
    public class ActivityHelper : IActivityHelper
    {
        internal readonly IDaprClientFactory daprClientFactory;
        public ActivityHelper(IDaprClientFactory daprClientFactory)
        {
            this.daprClientFactory = daprClientFactory;
        }

        public async Task<bool> PublishActivity(ActivityMessageDTO activityMessage)
        {
            return await daprClientFactory.PublishToDapr(DaprActivityConfig.PUBSUB, DaprTopics.Activity, activityMessage);
        }
        public async Task<bool> PublishActivityOfValueType<T>(ActivityDTO<T> activity)
        {
            return await daprClientFactory.PublishToDapr(DaprActivityConfig.PUBSUB, DaprTopics.Activity, activity);
        }
        public async Task<bool> PublishToTopic<T>(string topic, T value)
        {
            return await daprClientFactory.PublishToDapr(DaprActivityConfig.PUBSUB, topic, value);
        } 
        public async Task<bool> PublishToTopic<T>(string topic, string value)
        {
            return await daprClientFactory.PublishToDapr(DaprActivityConfig.PUBSUB, topic, value);
        } 
    }

   
}
