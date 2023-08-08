namespace SimpleEcommerceAspNet6.Models
{
    public class Shipper
    {
        public int ShipperId { get; set; }
        public string ShipperName { get; set; }
        public string Phone { get; set; }
        public string? Company { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
