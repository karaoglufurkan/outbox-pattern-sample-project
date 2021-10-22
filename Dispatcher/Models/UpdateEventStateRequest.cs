using System;
using Shared.Models;

namespace Dispatcher.Models
{
    public class UpdateEventStateRequest
    {
        public Guid EventId { get; set; }
        public OutboxEventState State { get; set; }
    }
}