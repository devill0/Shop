using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.Domain;

namespace Shop.Web.Controllers
{
    public class ProductsController : Controller
    {
        private static readonly List<Product> products = new List<Product>
        {
            new Product("Laptop", "Electronics", 3000),
            new Product("Jeans", "Trousers", 150),
            new Product("Hammer", "Tools", 50),
            new Product("Ticket", "Cinema", 20)
        };

        public IActionResult Index()
        {
            return View(products);
        }
    }
}