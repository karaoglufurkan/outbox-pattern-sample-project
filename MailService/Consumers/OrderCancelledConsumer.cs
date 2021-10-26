using System.Threading.Tasks;
using MailService.Data;
using MailService.Models;
using MassTransit;
using Shared.Events;

namespace MailService.Consumers
{
    public class OrderCancelledConsumer : IConsumer<OrderCancelled>
    {
        private readonly MailServiceDbContext _context;

        public OrderCancelledConsumer(MailServiceDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<OrderCancelled> context)
        {
            await _context.MailQueue.AddAsync(new MailQueueItem
            {
                Email = context.Message.Email,
                Title = "Order has been cancelled!",
                Body = $"User Id: {context.Message.UserId}"
            });
            await _context.SaveChangesAsync();
        }
    }
}