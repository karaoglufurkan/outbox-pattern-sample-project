namespace OutboxService.Models
{
    public class OutboxEventRequest
    {
        public object Message { get; set; }
        public string Type { get; set; }
    }
}