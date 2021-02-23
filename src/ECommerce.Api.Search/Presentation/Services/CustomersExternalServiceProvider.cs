using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ECommerce.Api.Search.Presentation.ServiceModels.CustomerServiceModels.ServiceResults;
using ECommerce.Api.Search.Presentation.Services.Contracts;
using Microsoft.Extensions.Logging;

namespace ECommerce.Api.Search.Presentation.Services
{
    public class CustomersExternalServiceProvider : ICustomersService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CustomersExternalServiceProvider> _logger;
        
        public CustomersExternalServiceProvider(IHttpClientFactory httpClientFactory, 
            ILogger<CustomersExternalServiceProvider> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        
        public async Task<CustomerServiceResult> GetCustomerAsync(int id)
        {
            var client = _httpClientFactory.CreateClient(nameof(CustomersExternalServiceProvider));
            try
            {
                var response = await client.GetAsync($"api/v1/customers/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() {PropertyNameCaseInsensitive = true};
                    var result = JsonSerializer.Deserialize<CustomerServiceResult>(content,options);
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