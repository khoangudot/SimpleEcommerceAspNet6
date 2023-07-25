namespace SimpleEcommerceAspNet6.Data
{
    public class Customer:User
    {
        public virtual ICollection<DeliveryAddress>? DeliveryAddresses { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
