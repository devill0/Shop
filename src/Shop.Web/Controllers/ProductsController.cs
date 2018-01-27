using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Shop.Web.Models;
using Shop.Core.Services;
using Shop.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Shop.Web.Framework;

namespace Shop.Web.Controllers
{
    [Route("products")]
    [CookieAuth("require-admin")]
    public class ProductsController : Controller
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService; //wstrzykiwanie wartości przez konstruktor
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var products = productService
                .GetAll()
                .Select(p => new ProductViewModel(p));

            return View(products);
        }

        [HttpGet("add")]
        public IActionResult AddProduct()
        {
            var viewModel = new AddOrUpdateProductViewModel();

            return View(viewModel);
        }

        [HttpPost("add")]
        public IActionResult AddProduct(AddOrUpdateProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            productService.Add(viewModel.Name, viewModel.Category, viewModel.Price);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/update")]
        public IActionResult Update(Guid id)
        {
            var product = productService.Get(id);
            if(product == null)
            {
                return NotFound(); //404
            }
            var viewModel = new AddOrUpdateProductViewModel(product);

            return View(viewModel);
        }

        [HttpPost("{id}/update")]
        public IActionResult Update(AddOrUpdateProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            productService.Update(new ProductDTO
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Category = viewModel.Category,
                Price = viewModel.Price
            });

            return RedirectToAction(nameof(Index));
        }
    }
}