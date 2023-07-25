namespace SimpleEcommerceAspNet6.Data
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string? Description { get; set; }
        public bool Published { get; set; }
        public virtual ICollection<Product>? Products { get; set; }

    }
}