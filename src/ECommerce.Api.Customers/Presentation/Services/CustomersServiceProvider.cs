using AutoMapper;
using ECommerce.Api.Customers.Domain;
using ECommerce.Api.Customers.Domain.QueryModels.CustomerQueryModels.QueryParams;
using ECommerce.Api.Customers.Domain.Repositories.Contracts;
using ECommerce.Api.Customers.Presentation.Services.Contracts;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Customers.Presentation.Infrastructure;
using ECommerce.Api.Customers.Presentation.ServiceModels.CustomerServiceModels.ServiceResults;
using ECommerce.Utilities.DomainHelpers;
using ECommerce.Utilities.PresentationHelpers.Services;

namespace ECommerce.Api.Customers.Presentation.Services
{
    public class CustomersServiceProvider : BaseService, ICustomersService
    {
        private readonly IMemoryCache _cache;
        private readonly ICustomersRepository _customersRepository;
        private readonly ILogger<CustomersServiceProvider> _logger;
        private readonly IMapper _mapper;

        public CustomersServiceProvider(
            IUnitOfWork uow,
            IMemoryCache cache,
            ICustomersRepository customersRepository,
            ILogger<CustomersServiceProvider> logger,
            IMapper mapper) : base(uow)
        {
            _cache = cache;
            _customersRepository = customersRepository;
            _logger = logger;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }


        public async Task<IEnumerable<CustomerServiceResult>> GetCustomersAsync(CustomersQuery qp)
        {
            try
            {
                // Here I list the query result from cache if they exist, but now the data can vary according to the category ID, page and amount of
                // items per page. I have to compose a cache to avoid returning wrong data.
                string cacheKey = GetCacheKeyForCustomersQuery(qp);

                var entities = await _cache.GetOrCreateAsync(cacheKey, (entry) => {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                    return _customersRepository.ListPaginationAsync(qp);
                });

                var models = entities != null && entities.Any()
                    ? _mapper.Map(entities, new List<CustomerServiceResult>())
                    : new List<CustomerServiceResult>();

                return models;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                throw;
            }

        }

        public async Task<CustomerServiceResult> GetCustomerAsync(string id)
        {
            try
            {
                var entity = await _customersRepository.FindByIdAsReadableAsync(id);
                var model = entity != null
                    ? _mapper.Map(entity, new CustomerServiceResult())
                    : null;
                return model;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<int> GetCustomersCountAsync()
        {
            try
            {
                return await _customersRepository.CountAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                throw;
            }
        }

        private string GetCacheKeyForCustomersQuery(CustomersQuery query)
        {
            string key = CacheKeys.CustomersList.ToString();

            if (!string.IsNullOrEmpty(query.Search))
            {
                key = string.Concat(key, "_", query.Search);
            }

            key = string.Concat(key, "_", query.Limit, "_", query.Offset);
            return key;
        }
    }
}
