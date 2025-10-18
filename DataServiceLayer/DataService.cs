
using System.Collections.Generic;
using System.Linq;
using DataServiceLayer.DTO;
using DataServiceLayer.Model;
using Microsoft.EntityFrameworkCore;

namespace DataServiceLayer;

public class DataService : IDataService
{

  private readonly DatabaseContext _db;

  public DataService()
  {
    _db = new DatabaseContext();

  }

  public Product GetProduct(int productId)
  {
    var product = _db.Products.Select(x =>
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
    var productList = _db.Products
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
    var productList = _db.Products
    .Where(x => x.Name.ToLower().Contains(searchKeyword.ToLower()))
    .Select(x => new ProductCategoryName
    {
      ProductName = x.Name,
      CategoryName = x.Category.Name,

    })
    .ToList();
    return productList;

  }
}



