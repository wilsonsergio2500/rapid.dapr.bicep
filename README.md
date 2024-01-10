<h1>
<img src="https://dapr.io/images/dapr.svg" width="50px">
Rapid Dapr
</h1>


Rapid Pub/Sub Approach to aid in getting started with Dapr, .NET and Bicep.

## The idea

In a previous repo, A Pub/Sub model was introduced but having in mind 
the goal of bringing about a great development experience. An integrated development experience was not always the case in the plethora of Dapr examples
that inhabit the internet.

However, In this repo I take a step further into actualizing, bringing the actual 
solution to market, or in other words live. A great inspiration to this idea was [awesome-azd.](https://azure.github.io/awesome-azd/?name=dapr)
While the templates in this portal are compelling, There was an envisioned use case that was not to be found there. 
This use case involved the following ingredients: Azure Service Bus - Multiple Topics, Multiple Subscriptions, .NET Shared Class Libraries -
with a common library for avoiding magical strings on topics.

This boilerplate uses the concept of bicep templates and the azd cli toolkit
to bring to production a working solution. The bicep templates are used to create the infrastructure and the azd cli toolkit 
is used to deploy the application. 

## Enhance

Introduce or remove additional topics in the servicebus.bicep file. 
For instances under the *serviceBusNamespace* resource, topics could be define as follows:

```bicep
param activityTopic string = 'activity'
param subName string = 'sub'

resource serviceBusNamespace 'Microsoft.ServiceBus/namespaces@2021-06-01-preview' = {
  name: serviceBusNamespaceName
  location: location
  .....

  resource topic 'topics' = {
    name: activityTopic
    properties: {
      supportOrdering: true
    }

    resource subscription 'subscriptions' = {
      name: subName
      properties: {
        deadLetteringOnFilterEvaluationExceptions: true
        deadLetteringOnMessageExpiration: true
        maxDeliveryCount: 10
      }
    }
  }
}
```
Finally, ehance the Subscriber and Publisher controllers to produce and emit the
desired event based on the subscriptions. 

#### Publisher

```csharp
        [HttpPost(DaprTopics.Activity)]
        public async Task<IActionResult> PostMessage([FromBody] ActivityMessageDTO message)
        {
            Console.WriteLine($"Calling Publish Action:- {message.Key}, DateTime: - {DateTime.Now}");
            await activityHelper.PublishToTopic(DaprTopics.Activity, message);
            return Ok();
        }
```
#### Subscriber

```csharp
        [Topic(DaprActivityConfig.PUBSUB, DaprTopics.Activity)]
        [HttpPost(DaprTopics.Test)]
        public IActionResult Post([FromBody] ActivityMessageDTO message)
        {
            Console.WriteLine($"Published topic:{DaprTopics.Test} Action:- {message.Key}, DateTime: - {DateTime.Now}");
            return Ok();
        }
``` 

## Deploy

Thanks to the azd cli toolkit, the deployment of the application is as simple as running the following command:

```bash
azd login
azd up
```