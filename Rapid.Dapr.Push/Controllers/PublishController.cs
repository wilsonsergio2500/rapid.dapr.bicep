using Microsoft.AspNetCore.Mvc;
using Rapid.Dapr.Push.Services;
using Rapid.Dapr.Task.Core.DTOs;
using Rapid.Dapr.Task.Core.Helpers;

namespace Rapid.Dapr.Push.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PublishController : ControllerBase
    {
        internal readonly IActivityHelper activityHelper;
        public PublishController(IActivityHelper activityHelper)
        {
            this.activityHelper = activityHelper;
        }
        [HttpPost("message")]
        public async Task<IActionResult> PostMessage([FromBody] ActivityMessageDTO message)
        {
            Console.WriteLine($"Calling Publish Action:- {message.Key}, DateTime: - {DateTime.Now}");
            await activityHelper.PublishActivity(message);
            return Ok();
        }
        [HttpPost("test")]
        public async Task<IActionResult> PostTest([FromBody] ActivityMessageDTO message)
        {
            Console.WriteLine($"Calling Publish topic: {DaprTopics.Test} Action:- {message.Key}, DateTime: - {DateTime.Now}");
            await activityHelper.PublishToTopic(DaprTopics.Test, message);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ActivityMessageDTO message)
        {
            Console.WriteLine($"Calling Publish Action:- {message.Key}, DateTime: - {DateTime.Now}");
            await activityHelper.PublishActivity(message);
            return Ok();
        }
    }
}
