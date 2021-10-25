using System.Threading.Tasks;
using MailService.Data;
using MailService.Models;
using MassTransit;
using Shared.Events;

namespace MailService.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        private readonly MailServiceDbContext _context;
        public OrderCreatedConsumer(MailServiceDbContext context)
        {
            _context = context;

        }

        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            await _context.MailQueue.AddAsync(new MailQueueItem
            {
                Email = context.Message.Email,
                Title = "Order has been created!",
                Body = $"User Id: {context.Message.UserId}\n" +
                $"Total price: {context.Message.TotalPrice}"
            });
            await _context.SaveChangesAsync();
        }
    }
}