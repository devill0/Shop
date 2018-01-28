using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop.Web.Framework
{
    public class Authenticator : IAuthenticator
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public Authenticator(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task SignInAsync(ClaimsPrincipal principal)
        {
            await httpContextAccessor.HttpContext
                .SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

        public async Task SignOutAsync()
        {
            await httpContextAccessor.HttpContext.SignOutAsync();
        }
    }
}
