using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.Domain;
using Shop.Web.Models;

namespace Shop.Web.Controllers
{
    [Route("products")]
    public class ProductsController : Controller
    {
        private static readonly List<Product> products = new List<Product>
        {
            new Product("Laptop", "Electronics", 3000),
            new Product("Jeans", "Trousers", 150),
            new Product("Hammer", "Tools", 50),
            new Product("Ticket", "Cinema", 20)
        };

        [HttpGet]
        public IActionResult Index()
        {
            return View(products);
        }

        [HttpGet("add")]
        public IActionResult AddProduct()
        {
            var viewModel = new ProductViewModel();

            return View(viewModel);
        }

        [HttpPost("add")]
        public IActionResult AddProduct(ProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            products.Add(new Product(viewModel.Name, viewModel.Category, viewModel.Price));

            return RedirectToAction(nameof(Index));
        }
    }
}