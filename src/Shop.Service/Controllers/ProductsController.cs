using Microsoft.AspNetCore.Mvc;
using Shop.Core.Services;

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
            var products = productService.GetAll();

            return Ok(products);
        }
    }
}