namespace SimpleEcommerceAspNet6.Models
{
    public class DeliveryAddress
    {
        public int DeliveryAddressId { get; set; }
        public string DeliveryAddressDescription { get; set; }
        public Customer Customer { get; set; }
    }
}
