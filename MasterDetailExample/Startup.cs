using System;
using System.Collections.Generic;
using MasterDetailExample.Context;
using MasterDetailExample.Models;
using MasterDetailExample.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MasterDetailExample
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
            var uniqueDatabase = Guid.NewGuid().ToString();
            services.AddDbContext<MyCompanyContext>(opt => opt.UseInMemoryDatabase(uniqueDatabase));

            services.AddMvc();

            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ICartItemRepository, CartItemRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            
            services.AddTransient<DbContext, MyCompanyContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, 
            IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // Grab the new context from the service provider that was injected (DI)
            var context = serviceProvider.GetService<MyCompanyContext>();
            InitializeDatabase(context);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void InitializeDatabase(MyCompanyContext context)
        {
            var customer1 = new Customer
            {
                CustomerId = 1,
                Name = "Smitty Werbenjagermanjensen"
            };

            context.Customers.Add(customer1);
            context.SaveChanges();

            context.Cart.Add(
                new Cart
                {
                    CartId = 1, CustomerId = 1
                });
            
            context.CartItems.AddRange(
                new List<CartItem>
                {
                    new CartItem{ CartId = 1, ProductId = 3, Quantity = 1 },
                    new CartItem{ CartId = 1, ProductId = 2, Quantity = 1},
                    new CartItem{ CartId = 1, ProductId = 4, Quantity = 2 },
                    new CartItem{ CartId = 1, ProductId = 10, Quantity = 1 },
                    new CartItem{ CartId = 1, ProductId = 11, Quantity = 1 }
                }    
            );
            
            context.Products.AddRange(
                new List<Product>
                {
                    new Product { ProductId = 1, Title="Old Spice", Price = new decimal(3.99), InStock = true},
                    new Product { ProductId = 2, Title="Milk", Price = new decimal(1.50), InStock = true},
                    new Product { ProductId = 3, Title="Bread", Price = new decimal(1.20), InStock = true},
                    new Product { ProductId = 4, Title="Wine", Price = new decimal(10.99), InStock = true},
                    new Product { ProductId = 5, Title="Lettuce", Price = new decimal(1.00), InStock = false},
                    new Product { ProductId = 6, Title="Bananas", Price = new decimal(1.50), InStock = true},
                    new Product { ProductId = 7, Title="Beer", Price = new decimal(10), InStock = true},
                    new Product { ProductId = 8, Title="Pepsi", Price = new decimal(5.99), InStock = true},
                    new Product { ProductId = 9, Title="Cereal", Price = new decimal(3.00), InStock = true},
                    new Product { ProductId =10, Title="Die Hard DVD", Price = new decimal(5.99), InStock = false},
                    new Product { ProductId =11, Title="#1 Soda-Drinking Hat", Price = new decimal(9.99), InStock = false}
                }
            );

            context.SaveChanges();

        }
    }
}
