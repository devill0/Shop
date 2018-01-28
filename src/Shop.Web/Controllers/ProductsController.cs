using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Shop.Web.Models;
using Shop.Core.Services;
using Shop.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Shop.Web.Framework;
using System.Threading.Tasks;

namespace Shop.Web.Controllers
{
    [Route("products")]
    [CookieAuth("require-admin")]
    public class ProductsController : Controller
    {
        private readonly IProductService productService;
        private readonly IServiceClient serviceClient;

        public ProductsController(IProductService productService, IServiceClient serviceClient)
        {
            this.productService = productService;
            this.serviceClient = serviceClient;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            //var products = productService
            //    .GetAll()
            //    .Select(p => new ProductViewModel(p));
            var products = await serviceClient.GetProductAsync();
            var viewModels = products.Select(p => new ProductViewModel(p));

            return View(viewModels);
        }

        [HttpGet("add")]
        public IActionResult AddProduct()
        {
            var viewModel = new AddOrUpdateProductViewModel();

            return View(viewModel);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProduct(AddOrUpdateProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await serviceClient.AddProductAsync(viewModel.Name, viewModel.Category, viewModel.Price);

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