using Dapr;
using Microsoft.AspNetCore.Mvc;
using Rapid.Dapr.Task.Core.DTOs;
using Rapid.Dapr.Task.Core.Helpers;

namespace Rapid.Dapr.Sub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActivityController : ControllerBase
    {
        public ActivityController()
        {
        }

        [Topic(DaprActivityConfig.PUBSUB, DaprTopics.Activity)]
        [HttpPost(DaprTopics.Activity)]
        public Task<ActionResult<bool>> PublishedMessage(ActivityMessageDTO activityMessageDto)
        {
            Console.WriteLine($"Published topic:@{DaprTopics.Activity}:- {activityMessageDto.Key}, DateTime: - {DateTime.Now}");
            return System.Threading.Tasks.Task.FromResult<ActionResult<bool>>(true);
        }
        
        [Topic(DaprActivityConfig.PUBSUB, DaprTopics.Test)]
        [HttpPost(DaprTopics.Test)]
        public IActionResult Post([FromBody] ActivityMessageDTO message)
        {
            Console.WriteLine($"Published topic:{DaprTopics.Test} Action:- {message.Key}, DateTime: - {DateTime.Now}");
            return Ok();
        }   
    }
}
