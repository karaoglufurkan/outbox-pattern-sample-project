namespace OrderService.Models
{
    public class CreateOrderRequestModel
    {
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }
    }
    
}