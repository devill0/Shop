using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.Domain;
using Shop.Web.Models;
using Shop.Core.Repositories;
using Shop.Core.Services;

namespace Shop.Web.Controllers
{
    [Route("products")]
    public class ProductsController : Controller
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService; //wstrzykiwanie wartości przez konstruktor
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = productService
                .GetAll()
                .Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                Price = p.Price
            });

            return View(products);
        }

        [HttpGet("add")]
        public IActionResult AddProduct()
        {
            var viewModel = new AddProductViewModel();

            return View(viewModel);
        }

        [HttpPost("add")]
        public IActionResult AddProduct(AddProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            productService.Add(viewModel.Name, viewModel.Category, viewModel.Price);

            return RedirectToAction(nameof(Index));
        }
    }
}