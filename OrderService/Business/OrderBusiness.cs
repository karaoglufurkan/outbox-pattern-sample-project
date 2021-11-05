using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Models;
using Shared.Events;
using Shared.Models;

namespace OrderService.Business
{
    public class OrderBusiness : IOrderBusiness
    {
        private readonly OrderDbContext _context;

        public OrderBusiness(OrderDbContext context)
        {
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

        public async Task<bool> CreateOrderAsync(CreateOrderRequestModel request)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var newOrder = new Order
                    {
                        UserId = request.UserId,
                        TotalPrice = request.TotalPrice,
                        Email = request.Email
                    };
                    await _context.Orders.AddAsync(newOrder);
                    await _context.SaveChangesAsync();

                    var newEvent = new OutboxEvent(
                        new OrderCreated
                        {
                            OrderId = newOrder.Id,
                            UserId = request.UserId,
                            Email = request.Email,
                            TotalPrice = request.TotalPrice
                        },
                        Guid.NewGuid(),
                        DateTime.Now);
                    await _context.OutboxEvents.AddAsync(newEvent);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return true;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return false;
                }
            }
        }

        public async Task CancelOrderAsync(int orderId)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(x => x.Id == orderId && !x.IsCancelled);
            if (order == null)
            {
                throw new Exception("Order not found!");
            }
            order.IsCancelled = true;

            await _context.OutboxEvents.AddAsync(
                new OutboxEvent(
                    new OrderCancelled
                    {
                        OrderId = orderId,
                        Email = order.Email,
                        UserId = order.UserId
                    },
                    Guid.NewGuid(),
                    DateTime.Now
                )
            );

            await _context.SaveChangesAsync();
        }
    }
}