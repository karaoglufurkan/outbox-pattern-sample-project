namespace MailService.Models
{
    public class MailQueueItem
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public MailQueueItemState State { get; set; }
    }
}