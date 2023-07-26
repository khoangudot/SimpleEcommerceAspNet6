using Microsoft.EntityFrameworkCore;

namespace SimpleEcommerceAspNet6.Data
{
    public class EcommerceDbContext:DbContext
    {
        public EcommerceDbContext()
        {

        }

        public EcommerceDbContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<DeliveryAddress> DeliveryAddresses { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<TransactStatus> TransactStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u=>u.UserId);
            modelBuilder.Entity<Role>().HasKey(r => r.RoleId);
            modelBuilder.Entity<Role_User>().HasKey(ru => new { ru.RoleId, ru.UserId });
            modelBuilder.Entity<Product>().HasKey(p => p.ProductId);
            modelBuilder.Entity<Category>().HasKey(c => c.CategoryId);
            modelBuilder.Entity<DeliveryAddress>().HasKey(d => d.DeliveryAddressId);
            modelBuilder.Entity<Order>().HasKey(o => o.OrderId);
            modelBuilder.Entity<OrderDetail>().HasKey(od => new {od.OrderId,od.ProductId});
            modelBuilder.Entity<Shipper>().HasKey(s => s.ShipperId);
            modelBuilder.Entity<TransactStatus>().HasKey(t => t.TransactStatusId);

            modelBuilder.Entity<Role_User>()
                .HasOne(ru=>ru.User)
                .WithMany(ru=>ru.Role_Users) 
                .HasForeignKey(ru=>ru.UserId);

            modelBuilder.Entity<Role_User>()
               .HasOne(ru => ru.Role)
               .WithMany(ru => ru.Role_Users)
               .HasForeignKey(ru => ru.RoleId);

            modelBuilder.Entity<Product>()
               .HasOne(p => p.category)
               .WithMany(p => p.Products)
               .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Order>()
              .HasOne(o => o.Customer)
              .WithMany(o => o.Orders)
              .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o=>o.OrderDetails)
                .HasForeignKey(od=> od.OrderId);

            modelBuilder.Entity<OrderDetail>()
               .HasOne(od => od.Product)
               .WithMany(p => p.OrderDetails)
               .HasForeignKey(od => od.ProductId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.TransactStatus)
                .WithMany(t => t.Orders)
                .HasForeignKey(o => o.TransactStatusId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Shipper)
                .WithMany(t => t.Orders)
                .HasForeignKey(o => o.ShipperId);


            base.OnModelCreating(modelBuilder);
        }
    }
}
