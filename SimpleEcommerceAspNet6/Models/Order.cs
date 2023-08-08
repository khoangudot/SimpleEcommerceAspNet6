using System.ComponentModel.DataAnnotations;

namespace SimpleEcommerceAspNet6.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public int TransactStatusId { get; set; }
        public bool? Deleted { get; set; }
        public bool? Paid { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int? TotalPrice { get; set; }
        public int? PaymentId { get; set; }
        public string? Note { get; set; }
        public string? DeliveryAddress { get; set; }
        public int? ShipperId { get; set; }
        public virtual Shipper? Shipper { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual TransactStatus? TransactStatus { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
