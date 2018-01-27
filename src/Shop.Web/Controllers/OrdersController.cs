using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.DTO;
using Shop.Core.Services;
using Shop.Web.Framework;
using Shop.Web.Models;
using System;
using System.Collections.Generic;

namespace Shop.Web.Controllers
{
    [CookieAuth]
    public class OrdersController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        [HttpGet("orders")]
        public IActionResult Index()
        {
            var orders = orderService.Browse(CurrentUserId);
            var viewModels = mapper.Map<IEnumerable<OrderViewModel>>(orders);

            return View(viewModels);
        }

        [HttpPost("orders")]
        public IActionResult Create()
        {
            try
            {
                orderService.Create(CurrentUserId);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            
        }
    }
}