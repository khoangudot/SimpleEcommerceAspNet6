

using System.ComponentModel.DataAnnotations;

namespace SimpleEcommerceAspNet6.Data
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string? SortDescription { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public string? Thumbnail { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? HomeFlag { get; set; }
        public bool? Active { get; set; }
        public int? UnitInStock { get; set; }
        public Category category { get; set; }
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
