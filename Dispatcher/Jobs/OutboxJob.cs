using System.Linq;
using System.Threading.Tasks;
using Dispatcher.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;
using Shared.Models;

namespace Dispatcher.Jobs
{
    [DisallowConcurrentExecution]
    public class OutboxJob : IJob
    {
        private readonly ILogger<OutboxJob> _logger;
        private readonly DispatcherDbContext _context;
        private readonly IPublishEndpoint _publisher;

        public OutboxJob(ILogger<OutboxJob> logger, DispatcherDbContext context, IPublishEndpoint publisher)
        {
            _logger = logger;
            _context = context;
            _publisher = publisher;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var messages = await _context.OutboxEvents
                .Where(x => x.State == OutboxEventState.ReadyToSend)
                .ToListAsync();

            foreach (var message in messages)
            {
                await _publisher.Publish(message.RecreateMessage());
                _logger.LogInformation("Event published");

                var record = await _context.OutboxEvents
                    .FirstOrDefaultAsync(x => x.EventId == message.EventId);
                record.ChangeState(OutboxEventState.SendToQueue);
                _context.SaveChanges();
                _logger.LogInformation("States updated");
            }
        }
    }
}