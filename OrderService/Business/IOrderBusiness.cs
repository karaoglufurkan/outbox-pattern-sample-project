using System.Collections.Generic;
using System.Threading.Tasks;
using OrderService.Models;

namespace OrderService.Business
{
    public interface IOrderBusiness
    {
        Task<List<Order>> GetOrdersAsync();
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<bool> CreateOrderAsync(CreateOrderRequestModel request);
        Task CancelOrderAsync(int orderId);

    }
}