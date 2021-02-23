using AutoMapper;
using ECommerce.Api.Orders.Domain.QueryModels.OrderQueryModels.QueryParams;
using ECommerce.Api.Orders.Domain.Repositories.Contracts;
using ECommerce.Api.Orders.Presentation.Services.Contracts;
using ECommerce.Api.Orders.Presentation.ServiceModels.OrderServiceModels.ServiceResults;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Orders.Presentation.Infrastructure;
using ECommerce.Utilities.DomainHelpers;
using ECommerce.Utilities.PresentationHelpers.Services;

namespace ECommerce.Api.Orders.Presentation.Services
{
    public class OrdersServiceProvider : BaseService, IOrdersService
    {
        private readonly IMemoryCache _cache;
        private readonly IOrdersRepository _customersRepository;
        private readonly ILogger<OrdersServiceProvider> _logger;
        private readonly IMapper _mapper;

        public OrdersServiceProvider(
            IUnitOfWork uow,
            IMemoryCache cache,
            IOrdersRepository customersRepository,
            ILogger<OrdersServiceProvider> logger,
            IMapper mapper) : base(uow)
        {
            _cache = cache;
            _customersRepository = customersRepository;
            _logger = logger;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }


        public async Task<IEnumerable<OrderServiceResult>> GetOrdersAsync(OrdersQuery qp)
        {
            try
            {
                // Here I list the query result from cache if they exist, but now the data can vary according to the category ID, page and amount of
                // items per page. I have to compose a cache to avoid returning wrong data.
                string cacheKey = GetCacheKeyForOrdersQuery(qp);

                var entities = await _cache.GetOrCreateAsync(cacheKey, (entry) => {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                    return _customersRepository.ListPaginationAsync(qp);
                });

                var models = entities != null && entities.Any()
                    ? _mapper.Map(entities, new List<OrderServiceResult>())
                    : new List<OrderServiceResult>();

                return models;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                throw;
            }

        }

        public async Task<OrderServiceResult> GetOrderAsync(string id)
        {
            try
            {
                var entity = await _customersRepository.FindByIdAsReadableAsync(id);
                var model = entity != null
                    ? _mapper.Map(entity, new OrderServiceResult())
                    : null;
                return model;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<int> GetOrdersCountAsync(int customerId)
        {
            try
            {
                return await _customersRepository.CountAsync(customerId);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                throw;
            }
        }

        private string GetCacheKeyForOrdersQuery(OrdersQuery query)
        {
            string key = CacheKeys.OrdersList.ToString();

            if (!string.IsNullOrEmpty(query.Search))
            {
                key = string.Concat(key, "_", query.Search);
            }

            key = string.Concat(key, "_", query.CustomerId, "_", query.Limit, "_", query.Offset);
            return key;
        }
    }
}
