using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Web.Framework;
using Shop.Core.Repositories;
using Shop.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Shop.Core.DTO;
using Shop.Core.Options;

namespace Shop.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(); 
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartManager, CartManager>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IAuthenticator, Authenticator>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            //var mapper = AutoMapperConfig.GetMapper();
            //services.AddSingleton<IMapper>(mapper); // <IMapper> is not required
            services.AddSingleton(AutoMapperConfig.GetMapper());
            services.AddSingleton<IServiceClient, ServiceClient>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(c =>
                {
                    c.LoginPath = new PathString("/login");
                    c.AccessDeniedPath = new PathString("/forbidden"); //303 
                    c.ExpireTimeSpan = TimeSpan.FromDays(7);
                });
            services.AddAuthorization(a => a.AddPolicy("require-admin",
                p => p.RequireRole(RoleDTO.Admin.ToString())));
            services.AddMemoryCache();
            services.Configure<ServiceClientOptions>(Configuration.GetSection("serviceClient"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) // kolejność jest ważna
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseMyMiddleware();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
