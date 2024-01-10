using System.Text.Json;

namespace Rapid.Dapr.Task.Core.Helpers
{
    public static class ParserExtension
    {
        public static T? ToObject<T>(this string value)
        {
            return JsonSerializer.Deserialize<T>(value, new JsonSerializerOptions {  PropertyNameCaseInsensitive = true});
        }
    }
}
