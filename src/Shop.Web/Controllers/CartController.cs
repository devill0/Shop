using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using Microsoft.Extensions.Caching.Memory;
using Shop.Web.Models;
using System.Linq;
using Shop.Core.Services;
using AutoMapper;
using Shop.Web.Framework;

namespace Shop.Web.Controllers
{    
    [Route("cart")]
    [CookieAuth]
    public class CartController : BaseController
    {
        private readonly ICartService cartService;
        private readonly IMapper mapper;

        public CartController(ICartService cartService, IMapper mapper)
        {
            this.cartService = cartService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var cart = cartService.Get(CurrentUserId);
            var viewModel = mapper.Map<CartViewModel>(cart);

            return View(viewModel);
        }

        [HttpPost("items/{productId}/add")]
        public IActionResult Add(Guid productId)
        {
            cartService.AddProduct(CurrentUserId, productId);

            return RedirectToAction("Index", "Products");
        }
    }
}