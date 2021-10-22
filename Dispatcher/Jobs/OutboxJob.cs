using System.Collections.Generic;
using System.Threading.Tasks;
using Dispatcher.ExternalServices;
using Dispatcher.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using Quartz;
using Shared.Models;

namespace Dispatcher.Jobs
{
    public class OutboxJob : IJob
    {
        private readonly ILogger<OutboxJob> _logger;
        private readonly OutboxService _outboxService;
        private readonly IPublishEndpoint _publisher;

        public OutboxJob(ILogger<OutboxJob> logger, OutboxService outboxService, IPublishEndpoint publisher)
        {
            _logger = logger;
            _outboxService = outboxService;
            _publisher = publisher;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var messages = await _outboxService.GetUnpublishedEventsAsync();

            messages.ForEach(async message =>
            {
                await _publisher.Publish(message);
                _logger.LogInformation("Event published");
                await _outboxService.UpdateEventsAsync(new List<UpdateEventStateRequest>
                {
                    new UpdateEventStateRequest
                    {
                        EventId = message.EventId,
                        State = OutboxEventState.SendToQueue
                    }
                });
                _logger.LogInformation("States updated");
            });
        }
    }
}