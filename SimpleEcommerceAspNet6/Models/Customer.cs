using System.ComponentModel.DataAnnotations;

namespace SimpleEcommerceAspNet6.Models
{

    public class Customer
    {
        public virtual ICollection<DeliveryAddress>? DeliveryAddresses { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
