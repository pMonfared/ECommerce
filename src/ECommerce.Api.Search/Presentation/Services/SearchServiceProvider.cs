using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ECommerce.Api.Search.Consts;
using ECommerce.Api.Search.Presentation.ServiceModels.OrderServiceModels.ServiceResults;
using ECommerce.Api.Search.Presentation.ServiceModels.ProductModels.ServiceResults;
using ECommerce.Api.Search.Presentation.Services.Contracts;
using ECommerce.Utilities.CustomExceptions;

namespace ECommerce.Api.Search.Presentation.Services
{
    public class SearchServiceProvider : ISearchService
    {
        private readonly IOrdersService _ordersService;
        private readonly IProductsService _productsService;
        private readonly ICustomersService _customersService;
        public SearchServiceProvider(IOrdersService ordersService, IProductsService productsService, ICustomersService customersService)
        {
            _ordersService = ordersService;
            _productsService = productsService;
            _customersService = customersService;
        }
        public async Task<dynamic> SearchAsync(int customerId)
        {
            if(customerId <= 0)
                throw new CustomArgumentOutOfRangeException($"The {nameof(customerId)} can not be zero or less than zer")
                {
                    ExceptionDetails = new ExceptionDetails(
                        SearchErrors.Search.CustomerIdOutOfRange.Message, 
                        SearchErrors.Search.CustomerIdOutOfRange.ErrorCode,
                        SearchErrors.Search.CustomerIdOutOfRange.Detail,
                        nameof(customerId))
                };

            var customer = await _customersService.GetCustomerAsync(customerId);
            
            if(customer == null)
                throw new CustomNotFoundException($"The {nameof(customerId)} cannnot find with this given id : <{customerId}>")
                {
                    ExceptionDetails = new ExceptionDetails(
                        nameof(customerId))
                };
            
            var orders = await _ordersService.GetOrdersAsync(customerId);

            var orderServiceResults = orders as OrderServiceResult[] ?? orders.ToArray();
            if (orderServiceResults.Any())
            {
                var products = await _productsService.GetProductsAsync();

                var productServiceResults = products?.ToList();
                foreach (var order in orderServiceResults)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productServiceResults != null && productServiceResults.Any()
                            ? productServiceResults?.FirstOrDefault(p => p.Id.Equals(item.ProductId))?.Name
                            : "Product information is not available";
                    }
                }
            }
            

            return new
            {
                customer,
                orders = orderServiceResults
            };
        }
    }
}