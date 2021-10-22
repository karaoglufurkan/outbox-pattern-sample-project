namespace Shared.Models
{
    public enum OutboxEventState
    {
        ReadyToSend = 1,
        SendToQueue = 2,
        Completed = 3,
    }
}