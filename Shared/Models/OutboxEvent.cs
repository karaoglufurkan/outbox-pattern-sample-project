using System;
using Newtonsoft.Json;

namespace Shared.Models
{
    public class OutboxEvent
    {
        protected OutboxEvent()
        {

        }

        public OutboxEvent(object message, Guid eventId, DateTime eventDate)
        {
            Data = JsonConvert.SerializeObject(message);
            Type = message.GetType().FullName + ", " +
                   message.GetType().Assembly.GetName().Name;
            EventId = eventId;
            EventDate = eventDate;
            State = OutboxEventState.ReadyToSend;
            ModifiedDate = DateTime.Now;
        }

        public long Id { get; protected set; }
        public string Data { get; protected set; }
        public string Type { get; protected set; }
        public Guid EventId { get; protected set; }
        public DateTime EventDate { get; protected set; }
        public OutboxEventState State { get; private set; }
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