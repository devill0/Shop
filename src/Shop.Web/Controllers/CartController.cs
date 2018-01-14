using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using Microsoft.Extensions.Caching.Memory;
using Shop.Web.Models;
using System.Linq;
using Shop.Core.Services;

namespace Shop.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Route("cart")]
    public class CartController : Controller
    {
        private readonly IMemoryCache cache;
        private readonly IProductService productService;

        public CartController(IMemoryCache cache, IProductService productService)
        {
            this.cache = cache;
            this.productService = productService;
        }

        public IActionResult Index()
        {
            var viewModel = cache.Get<CartViewModel>($"{User.Identity.Name}:cart");

            return View(viewModel);
        }

        [HttpPost("items/{productId}/add")]
        public IActionResult Add(Guid productId)
        {
            var product = productService.Get(productId);
            if (product == null)
            {
                return BadRequest();
            }

            var cart = cache.Get<CartViewModel>($"{User.Identity.Name}:cart");
            var cartItem = cart.Items.SingleOrDefault(i => i.ProductId == productId);
            if(cartItem == null)
            {
                cartItem = new CartItemViewModel
                {
                    ProductId = productId,
                    ProductName = product.Name,
                    UnitPrice = product.Price,
                    Quantity = 1
                };
                cart.Items.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }
            cache.Set($"{User.Identity.Name}:cart", cart);

            return Ok();
        }
    }
}