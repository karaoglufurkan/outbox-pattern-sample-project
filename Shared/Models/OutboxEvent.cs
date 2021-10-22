using System;
using Newtonsoft.Json;

namespace Shared.Models
{
    public class OutboxEvent
    {
        public OutboxEvent()
        {

        }

        public OutboxEvent(object message, string type, Guid eventId, DateTime eventDate)
        {
            Data = message.ToString();
            // Type = message.GetType().FullName + ", " +
            //        message.GetType().Assembly.GetName().Name;
            Type = type;
            EventId = eventId;
            EventDate = eventDate;
            State = OutboxEventState.ReadyToSend;
            ModifiedDate = DateTime.Now;
        }

        public long Id { get; set; }
        public string Data { get; set; }
        public string Type { get; set; }
        public Guid EventId { get; set; }
        public DateTime EventDate { get; set; }
        public OutboxEventState State { get; set; }
        public DateTime ModifiedDate { get; set; }

        public void ChangeState(OutboxEventState state)
        {
            State = state;
            this.ModifiedDate = DateTime.Now;
        }

        public object RecreateMessage() =>
                JsonConvert.DeserializeObject(Data, System.Type.GetType(Type));
    }
}