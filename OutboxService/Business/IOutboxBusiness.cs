using System.Collections.Generic;
using System.Threading.Tasks;
using OutboxService.Models;
using Shared.Models;

namespace OutboxService.Business
{
    public interface IOutboxBusiness
    {
         Task<List<OutboxEvent>> GetUnpublishedEvents();
         Task AddEvent(OutboxEvent request);
         Task UpdateEvents(List<UpdateEventStateRequest> request);
    }
}