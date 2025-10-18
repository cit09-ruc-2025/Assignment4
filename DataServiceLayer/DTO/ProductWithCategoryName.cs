using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataServiceLayer.Model;

namespace DataServiceLayer.DTO;

public class ProductWithCategoryName : Product
{
    public string CategoryName { get; set; }
}