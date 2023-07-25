namespace SimpleEcommerceAspNet6.Data
{
    public class DeliveryAddress
    {
        public int DeliveryAddressId { get; set; }
        public string DeliveryAddressDescription { get; set; }
        public Customer Customer { get; set; }
    }
}
