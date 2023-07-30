using System.ComponentModel.DataAnnotations;

namespace SimpleEcommerceAspNet6.Data
{

    public class Customer
    {
       
        public int CustomerId { get; set; }
        public virtual ICollection<DeliveryAddress>? DeliveryAddresses { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
