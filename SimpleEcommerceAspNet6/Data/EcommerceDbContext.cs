using Microsoft.EntityFrameworkCore;
using SimpleEcommerceAspNet6.Models;

namespace SimpleEcommerceAspNet6.Data
{
    public class EcommerceDbContext : DbContext
    {
        public EcommerceDbContext()
        {

        }

        public EcommerceDbContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Role_User> Role_Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<DeliveryAddress> DeliveryAddresses { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<TransactStatus> TransactStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<User>()
               .Property(u => u.Active)
               .HasDefaultValue(true);


            modelBuilder.Entity<Customer>().HasKey(c => c.UserId);
            modelBuilder.Entity<Customer>()
               .Property(c => c.UserId)
               .ValueGeneratedNever(); // UserId không được sinh tự động (vì là primary key và cùng giá trị với UserId trong bảng User)

            modelBuilder.Entity<Role>().HasKey(r => r.RoleId);
            modelBuilder.Entity<Role_User>().HasKey(ru => new { ru.RoleId, ru.UserId });
            modelBuilder.Entity<Product>().HasKey(p => p.ProductId);
            modelBuilder.Entity<Category>().HasKey(c => c.CategoryId);
            modelBuilder.Entity<DeliveryAddress>().HasKey(d => d.DeliveryAddressId);
            modelBuilder.Entity<Order>().HasKey(o => o.OrderId);
            modelBuilder.Entity<OrderDetail>().HasKey(od => new { od.OrderId, od.ProductId });
            modelBuilder.Entity<Shipper>().HasKey(s => s.ShipperId);
            modelBuilder.Entity<TransactStatus>().HasKey(t => t.TransactStatusId);

            modelBuilder.Entity<Role_User>()
                .HasOne(ru => ru.User)
                .WithMany(ru => ru.Role_Users)
                .HasForeignKey(ru => ru.UserId);

            modelBuilder.Entity<Role_User>()
               .HasOne(ru => ru.Role)
               .WithMany(ru => ru.Role_Users)
               .HasForeignKey(ru => ru.RoleId);

            modelBuilder.Entity<Product>()
                .Property(p => p.Discount)
                .HasDefaultValue(0m);

            modelBuilder.Entity<Product>()
               .HasOne(p => p.category)
               .WithMany(p => p.Products)
               .HasForeignKey(p => p.CategoryId);
               
            modelBuilder.Entity<Order>()
              .HasOne(o => o.Customer)
              .WithMany(o => o.Orders)
              .HasForeignKey(p => p.CustomerId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);

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


            modelBuilder.Entity<Customer>()
                .HasOne(c => c.User)
                .WithOne(u => u.Customer)
                .HasForeignKey<Customer>(c => c.UserId);

            base.OnModelCreating(modelBuilder);
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasData(new List<Role>
                {
                    new Role
                    {
                        RoleId = 1,
                        RoleName = "Admin",
                        Description="Quản Lý"
                    },
                        new Role
                    {
                        RoleId = 2,
                        RoleName = "Staff",
                        Description ="Nhân Viên"
                    },
                        new Role
                    {
                        RoleId = 3,
                        RoleName = "Customer",
                        Description = "Khách Hàng"
                    }
                });
            modelBuilder.Entity<Category>()
               .HasData(new List<Category>
               {
                    new Category
                    {
                        CategoryId = 1,
                        CategoryName = "Đồ dùng nhà bếp",
                        
                    },
                    new Category
                    {
                        CategoryId = 2,
                        CategoryName = "Đồ chơi trẻ em",
                       
                     },
                   new Category
                   {
                       CategoryId = 3,
                       CategoryName = "Dụng cụ học tập",
                        
                   }
               });
        }
    }
}
