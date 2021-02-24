using ECommerce.Api.Search.IoCConfig;
using ECommerce.Utilities.ErrorHandling.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace ECommerce.Api.Search
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
            var internalProductServiceAddress = Environment.GetEnvironmentVariable("InternalProductsServiceAddress")
                                                ?? throw new ArgumentNullException("InternalProductsServiceAddress", "Env: InternalProductsServiceAddress is null");
            var internalOrdersServiceAddress = Environment.GetEnvironmentVariable("InternalOrdersServiceAddress")
                                               ?? throw new ArgumentNullException("InternalOrdersServiceAddress","Env: InternalOrdersServiceAddress is null");
            var internalCustomersServiceAddress = Environment.GetEnvironmentVariable("InternalCustomersServiceAddress") 
                                               ?? throw new ArgumentNullException("InternalCustomersServiceAddress", "Env: InternalCustomersServiceAddress is null");

            services.AddAllServices(new ServiceConfigs { 
             InternalCustomersServiceAddress = internalCustomersServiceAddress,
             InternalOrdersServiceAddress = internalOrdersServiceAddress,
             InternalProductsServiceAddress = internalProductServiceAddress
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseErrorHandling();
            
            app.UseStatusCodePages();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            
        }
    }
}
