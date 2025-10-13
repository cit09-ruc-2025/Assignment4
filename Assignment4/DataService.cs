using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Assignment4
{
    public class DataService
    {
        private readonly DatabaseContext _db;

        public DataService()
        {
            _db = new DatabaseContext();

        }

        public Product GetProduct(int productId)
        {
            var product = _db.Products.Join(
                _db.Categories,
                product => product.CategoryId,
                category => category.Id,
                (product, category) => new Product
                {
                    Id = product.Id,
                    Name = product.Name,
                    Category = new Category
                    {
                        Name = category.Name
                    },
                    UnitPrice = product.UnitPrice,
                    QuantityPerUnit = product.QuantityPerUnit,
                    UnitsInStock = product.UnitsInStock

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
    }
}
