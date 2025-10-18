using System;
using DataServiceLayer.Model;
using Microsoft.EntityFrameworkCore;


namespace DataServiceLayer;

public class DatabaseContext : DbContext
{
    public DbSet<Category> Categories { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseNpgsql("host=localhost;db=northwind;uid=postgres;pwd=root");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().ToTable("categories");
        modelBuilder.Entity<Category>().Property(x => x.Id).HasColumnName("categoryid");
        modelBuilder.Entity<Category>().Property(x => x.Name).HasColumnName("categoryname");
        modelBuilder.Entity<Category>().Property(x => x.Description).HasColumnName("description");


        modelBuilder.Entity<Product>().ToTable("products");
        modelBuilder.Entity<Product>().Property(x => x.Id).HasColumnName("productid");
        modelBuilder.Entity<Product>().Property(x => x.Name).HasColumnName("productname");
        modelBuilder.Entity<Product>().Property(x => x.UnitPrice).HasColumnName("unitprice");
        modelBuilder.Entity<Product>().Property(x => x.CategoryId).HasColumnName("categoryid");
        modelBuilder.Entity<Product>().Property(x => x.QuantityPerUnit).HasColumnName("quantityperunit");
        modelBuilder.Entity<Product>().Property(x => x.UnitsInStock).HasColumnName("unitsinstock");

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("orders").HasKey(o => o.Id);
            entity.HasMany(o => o.OrderDetails).WithOne();

            entity.Property(o => o.Id).HasColumnName("orderid");
            entity.Property(o => o.Date).HasColumnName("orderdate");
            entity.Property(o => o.Required).HasColumnName("requireddate");
            entity.Property(o => o.ShipCity).HasColumnName("shipcity");
            entity.Property(o => o.ShipName).HasColumnName("shipname");
        });

        modelBuilder.Entity<OrderDetails>(entity =>
        {
            entity.ToTable("orderdetails").HasKey(od => new { od.OrderId, od.ProductId });
            entity.Property(od => od.OrderId).HasColumnName("orderid");
            entity.Property(od => od.ProductId).HasColumnName("productid");


            entity.HasOne(od => od.Order).WithMany(o => o.OrderDetails).HasForeignKey(od => od.OrderId);
            entity.HasOne(od => od.Product).WithMany().HasForeignKey(od => od.ProductId);

            entity.Property(od => od.UnitPrice).HasColumnName("unitprice");
            entity.Property(od => od.Quantity).HasColumnName("quantity");
            entity.Property(od => od.Discount).HasColumnName("discount");
        });
    }
}