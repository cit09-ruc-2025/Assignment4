using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataServiceLayer.DTO;
using DataServiceLayer.Model;

namespace DataServiceLayer
{
    public interface IDataService
    {
        Product GetProduct(int productId);
        List<ProductWithCategoryName> GetProductByCategory(int categoryId);
        List<ProductCategoryName> GetProductByName(string searchKeyword);
    }
}