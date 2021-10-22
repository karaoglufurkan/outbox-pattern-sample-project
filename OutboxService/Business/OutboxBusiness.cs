using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OutboxService.Data;
using OutboxService.Models;
using Shared.Models;

namespace OutboxService.Business
{
    public class OutboxBusiness : IOutboxBusiness
    {
        OutboxDbContext _context;
        public OutboxBusiness(OutboxDbContext context)
        {
            _context = context;
        }
        public async Task AddEvent(OutboxEvent request)
        {
            await _context.OutboxEvents.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OutboxEvent>> GetUnpublishedEvents()
        {
            var events = _context
                .OutboxEvents
                .Where(x => x.State == OutboxEventState.ReadyToSend)
                .OrderBy(x => x.Id);

            return await events.ToListAsync();
        }

        public async Task UpdateEvents(List<UpdateEventStateRequest> request)
        {
            foreach (var item in request)
            {
                var eventToUpdate = _context
                .OutboxEvents
                .Where(x => x.EventId == item.EventId)
                .FirstOrDefault();

                if (eventToUpdate == null)
                {
                    continue;
                }

                eventToUpdate.ChangeState(item.State);
            }

            await _context.SaveChangesAsync();
        }
    }
}