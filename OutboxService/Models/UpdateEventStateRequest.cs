using System;
using Shared.Models;

namespace OutboxService.Models
{
    public class UpdateEventStateRequest
    {
        public Guid EventId { get; set; }
        public OutboxEventState State { get; set; }
    }
}