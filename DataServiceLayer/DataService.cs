
using System.Collections.Generic;
using System.Linq;
using DataServiceLayer.DTO;
using DataServiceLayer.Model;
using Microsoft.EntityFrameworkCore;

namespace DataServiceLayer;

public class DataService : IDataService
{

    private readonly DatabaseContext db;

    public Product GetProduct(int productId)
    {
        var db = new DatabaseContext();

        var product = db.Products.Select(x =>
           new Product
           {
               Id = x.Id,
               Name = x.Name,
               Category = new Category
               {
                   Name = x.Category.Name
               },
               UnitPrice = x.UnitPrice,
               QuantityPerUnit = x.QuantityPerUnit,
               UnitsInStock = x.UnitsInStock
           }
        )
        .FirstOrDefault(x => x.Id == productId);
        return product;
    }


    public List<ProductWithCategoryName> GetProductByCategory(int categoryId)
    {
        var db = new DatabaseContext();

        var productList = db.Products
        .Where(x => x.CategoryId == categoryId)
        .Select(x => new ProductWithCategoryName
        {
            Id = x.Id,
            Name = x.Name,
            UnitPrice = x.UnitPrice,
            QuantityPerUnit = x.QuantityPerUnit,
            UnitsInStock = x.UnitsInStock,
            CategoryName = x.Category.Name
        })
        .ToList();
        return productList;
    }

    public List<ProductCategoryName> GetProductByName(string searchKeyword)
    {
        var db = new DatabaseContext();

        var productList = db.Products
        .Where(x => x.Name.ToLower().Contains(searchKeyword.ToLower()))
        .Select(x => new ProductCategoryName
        {
            ProductName = x.Name,
            CategoryName = x.Category.Name,

        })
        .ToList();
        return productList;

    }

    public List<Category> GetCategories()
    {
        using var context = new DatabaseContext();
        return context.Categories.ToList();
    }

    public Category GetCategory(int categoryId)
    {
        using var context = new DatabaseContext();
        return context.Categories.Find(categoryId);
    }

    public Category CreateCategory(string name, string description)
    {
        using var context = new DatabaseContext();
        var maxId = context.Categories.Max(c => c.Id);


        var categoryEntry = context.Add(new Category() { Id = ++maxId, Description = description, Name = name });
        context.SaveChanges();
        return categoryEntry.Entity;
    }

    public bool DeleteCategory(int categoryId)
    {
        using var context = new DatabaseContext();
        var category = context.Categories.Find(categoryId);
        if (category == null) return false;
        context.Remove(category);
        context.SaveChanges();
        return true;
    }

    public bool UpdateCategory(int categoryId, string name, string description)
    {
        using var context = new DatabaseContext();
        var category = context.Categories.Find(categoryId);
        if (category == null) return false;
        category.Name = name;
        category.Description = description;
        context.SaveChanges();
        return true;
    }

    public Order GetOrder(int orderId)
    {
        using var context = new DatabaseContext();
        var order = context.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Product).ThenInclude(p => p.Category).FirstOrDefault(o => o.Id == orderId);
        return order;

    }

    public List<Order> GetOrders()
    {
        using var context = new DatabaseContext();
        return context.Orders.ToList();
    }

    public List<OrderDetails> GetOrderDetailsByOrderId(int orderId)
    {
        using var context = new DatabaseContext();
        return context.OrderDetails.Include(od => od.Product).Where(od => od.OrderId == orderId).ToList();
    }
    public List<OrderDetails> GetOrderDetailsByProductId(int productId)
    {
        using var context = new DatabaseContext();
        return context.OrderDetails.Include(od => od.Order).Where(od => od.ProductId == productId).ToList();
    }
}



