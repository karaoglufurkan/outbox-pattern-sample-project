namespace Shared.Events
{
    public class OrderCancelled
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
    }
}