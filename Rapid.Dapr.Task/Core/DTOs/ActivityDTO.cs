namespace Rapid.Dapr.Task.Core.DTOs
{
    public class ActivityDTO<T>
    {
        public string Key { get; set; } = string.Empty;
        public T? Value { get; set; }
    }
}
