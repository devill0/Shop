using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Shop.Core.Services;
using Shop.Web.Framework;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService userService;
        private readonly ICartService cartService;
        private readonly IAuthenticator authenticator;

        public AccountController(IUserService userService, ICartService cartService, IAuthenticator authenticator)
        {
            this.userService = userService;
            this.cartService = cartService;
            this.authenticator = authenticator;
        }

        [HttpGet("login")]
        public IActionResult Login()
            => View();

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel) 
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            try
            {
                userService.Login(viewModel.Email, viewModel.Password);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);

                return View(viewModel);
            }
            var user = userService.Get(viewModel.Email);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await authenticator.SignInAsync(principal);
            cartService.Create(user.Id);
            
            return RedirectToAction("Index", "Cart");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            await HttpContext.SignOutAsync();
            try
            {
                cartService.Delete(CurrentUserId);
            }
            catch
            {
            }
            
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("register")]
        public IActionResult Register()
            => View(new RegisterViewModel());

        [HttpPost("register")]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            try
            {
                userService.Register(viewModel.Email, viewModel.Password, viewModel.Role);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);

                return View(viewModel);
            }

            return RedirectToAction(nameof(Login));
        }
    }
}