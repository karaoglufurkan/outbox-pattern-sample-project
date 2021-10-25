namespace ShipmentService.Models
{
    public class Shipment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public string Address { get; set; }
    }
}