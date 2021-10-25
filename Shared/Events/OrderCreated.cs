using System;

namespace Shared.Events
{
    public class OrderCreated
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string UserAddress { get; set; }
        public decimal TotalPrice { get; set; }
    }
}