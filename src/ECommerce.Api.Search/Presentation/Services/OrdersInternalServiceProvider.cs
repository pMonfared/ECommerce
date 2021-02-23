using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ECommerce.Api.Search.Presentation.ServiceModels.OrderServiceModels.ServiceResults;
using ECommerce.Api.Search.Presentation.Services.Contracts;
using ECommerce.Utilities.CustomExceptions;
using Microsoft.Extensions.Logging;
using ILogger = Serilog.ILogger;

namespace ECommerce.Api.Search.Presentation.Services
{
    public class OrdersExternalServiceProvider : IOrdersService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<OrdersExternalServiceProvider> _logger;
        
        public OrdersExternalServiceProvider(IHttpClientFactory httpClientFactory, 
            ILogger<OrdersExternalServiceProvider> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        
        public async Task<IEnumerable<OrderServiceResult>> GetOrdersAsync(int customerId)
        {
            var client = _httpClientFactory.CreateClient(nameof(OrdersExternalServiceProvider));
            try
            {
                var response = await client.GetAsync($"api/v1/orders/customer/{customerId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() {PropertyNameCaseInsensitive = true};
                    var result = JsonSerializer.Deserialize<IEnumerable<OrderServiceResult>>(content,options);
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _logger?.LogError(e.Message);
            }
            
            return new List<OrderServiceResult>();
        }
    }
}