using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Events;
using ShipmentService.Data;
using ShipmentService.Models;

namespace ShipmentService.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        private readonly ShipmentServiceDbContext _context;
        private readonly ILogger<OrderCreatedConsumer> _logger;
        
        public OrderCreatedConsumer(ShipmentServiceDbContext context, ILogger<OrderCreatedConsumer> logger)
        {
            _logger = logger;
            _context = context;
        }
        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            await _context.Shipments.AddAsync(new Shipment
            {
                OrderId = context.Message.OrderId,
                UserId = context.Message.UserId,
                Address = context.Message.UserAddress
            });
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Shipment created");
        }
    }
}