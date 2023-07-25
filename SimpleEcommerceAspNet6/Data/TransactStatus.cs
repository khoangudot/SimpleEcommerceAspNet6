namespace SimpleEcommerceAspNet6.Data
{
    public class TransactStatus
    {
        public int TransactStatusId { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
