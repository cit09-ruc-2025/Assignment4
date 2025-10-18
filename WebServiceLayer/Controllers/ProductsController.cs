using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataServiceLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebServiceLayer.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IDataService _dataService;

        public ProductsController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _dataService.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("category/{id}")]
        public IActionResult GetProductsByCategory(int id)
        {
            var products = _dataService.GetProductByCategory(id);
            if (products.Count < 1)
            {
                return NotFound(Array.Empty<object>());
            }
            return Ok(products);
        }

        [HttpGet("name/{keyword}")]
        public IActionResult SearchProduct(string keyword)
        {
            var products = _dataService.GetProductByName(keyword);
            if (products.Count < 1)
            {
                return NotFound(Array.Empty<object>());
            }
            return Ok(products);
        }

    }
}