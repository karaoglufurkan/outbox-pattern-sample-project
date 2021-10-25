using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderService.Business;
using OrderService.Data;
using OrderService.Models;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private IOrderBusiness _orderBusiness;
        public OrderController(IOrderBusiness orderBusiness)
        {
            _orderBusiness = orderBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await _orderBusiness.GetOrdersAsync());
        }

        [HttpGet]
        [Route("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var order = await _orderBusiness.GetOrderByIdAsync(orderId);
            return order == null ? NotFound() : Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderRequestModel request)
        {
            await _orderBusiness.CreateOrderAsync(request);
            return Ok();
        }
    }
}