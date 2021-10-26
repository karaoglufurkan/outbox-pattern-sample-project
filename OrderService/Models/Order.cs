using System.Collections.Generic;
using System.Linq;

namespace OrderService.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsCancelled { get; set; }
    }
}