namespace OrderService.Models
{
    public class CreateOrderRequestModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string UserAddress { get; set; }
        public decimal TotalPrice { get; set; }
    }
    
}