using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OrderService.Data;
using OrderService.Models;
using Shared.Events;
using Shared.Managers;

namespace OrderService.Business
{
    public class OrderBusiness : IOrderBusiness
    {
        private HttpClient _client;
        private OrderDbContext _context;

        public OrderBusiness(HttpClient client, OrderDbContext context)
        {
            _client = client;
            _context = context;
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context
            .Orders
            .FirstOrDefaultAsync(x => x.Id == orderId);
        }

        public async Task CreateOrderAsync(CreateOrderRequestModel request)
        {
            var newOrder = new Order
            {
                UserId = request.UserId,
                TotalPrice = request.TotalPrice
            };
            _context.Orders.Add(newOrder);
            _context.SaveChanges();

            await PublishCreatedOrder(newOrder);
        }

        private async Task PublishCreatedOrder(Order order)
        {
            var requestBody = new StringContent(
                JsonConvert.SerializeObject(new
                {
                    Message = new OrderCreated
                    {
                        OrderId = order.Id,
                        UserId = order.UserId,
                        TotalPrice = order.TotalPrice
                    },
                    Type = TypeManager.GetTypeString(typeof(OrderCreated))
                }),
                Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsync("http://localhost:6001/outbox", requestBody);
            response.EnsureSuccessStatusCode();
        }
    }
}