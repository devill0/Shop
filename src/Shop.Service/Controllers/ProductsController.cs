using Microsoft.AspNetCore.Mvc;
using Shop.Core.DTO;
using Shop.Core.Services;
using System;
using System.Linq;

namespace Shop.Service.Controllers
{
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var products = productService.GetAll().ToList();

            return Ok(products);
        }

        [HttpGet("{id}")] //nazwa parametru {}; 
        public IActionResult Get(Guid id)
        {
            var product = productService.Get(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody]ProductDTO product)
        {
            var productId = Guid.NewGuid();
            productService.Add(productId, product.Name, product.Category, product.Price);

            return Created($"products/{productId}", null);
        }
    }
}