using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Shop.Web.Framework
{
    public class MyMiddleware
    {
        private readonly RequestDelegate next;

        public MyMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine($"My middleware: {httpContext.Request.Path}");
            await next.Invoke(httpContext);
        }
    }
}
