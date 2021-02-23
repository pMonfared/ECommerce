using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ECommerce.Api.Search.Consts;
using ECommerce.Api.Search.Presentation.ServiceModels.OrderServiceModels.ServiceResults;
using ECommerce.Api.Search.Presentation.ServiceModels.ProductModels.ServiceResults;
using ECommerce.Api.Search.Presentation.Services.Contracts;
using ECommerce.Utilities.CustomExceptions;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ECommerce.Api.Search.Presentation.Services
{
    public class ProductsExternalServiceProvider : IProductsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ProductsExternalServiceProvider> _logger;
        
        public ProductsExternalServiceProvider(IHttpClientFactory httpClientFactory, ILogger<ProductsExternalServiceProvider> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductServiceResult>> GetProductsAsync()
        {

            var client = _httpClientFactory.CreateClient(nameof(ProductsExternalServiceProvider));
            try
            {
                var response = await client.GetAsync($"api/v1/products");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() {PropertyNameCaseInsensitive = true};
                    var result = JsonSerializer.Deserialize<IEnumerable<ProductServiceResult>>(content, options);
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _logger?.LogError(e.Message);
            }
            
            return new List<ProductServiceResult>();

        }

        public async Task<ProductServiceResult> GetProductAsync(int productId)
        {
            var client = _httpClientFactory.CreateClient(nameof(OrdersExternalServiceProvider));

            try
            {
                var response = await client.GetAsync($"api/v1/products/{productId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() {PropertyNameCaseInsensitive = true};
                    var result = JsonSerializer.Deserialize<ProductServiceResult>(content, options);
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _logger?.LogError(e.Message);
            }

            return null;
        }
    }
}