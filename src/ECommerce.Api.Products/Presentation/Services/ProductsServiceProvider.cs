using AutoMapper;
using ECommerce.Api.Products.Domain.QueryModels.ProductModels.QueryParams;
using ECommerce.Api.Products.Domain.Repositories.Contracts;
using ECommerce.Api.Products.Presentation.Services.Contracts;
using ECommerce.Api.Products.Presentation.Infrastructure;
using ECommerce.Api.Products.Presentation.Models.ProductModels.ServiceResults;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Utilities.DomainHelpers;
using ECommerce.Utilities.PresentationHelpers.Services;

namespace ECommerce.Api.Products.Presentation.Services
{
    public class ProductsServiceProvider : BaseService, IProductsService
    {

        private readonly IMemoryCache _cache;
        private readonly IProductsRepository _productsRepository;
        private readonly ILogger<ProductsServiceProvider> _logger;
        private readonly IMapper _mapper;

        public ProductsServiceProvider(
            IUnitOfWork uow,
            IProductsRepository productsRepository,
            ILogger<ProductsServiceProvider> logger,
            IMemoryCache cache,
            IMapper mapper) : base(uow)
        {
            _cache = cache;
            _productsRepository = productsRepository;
            _logger = logger;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }


        public async Task<IEnumerable<ProductServiceResult>> GetProductsAsync(ProductsQuery qp)
        {
            try
            {
                // Here I list the query result from cache if they exist, but now the data can vary according to the category ID, page and amount of
                // items per page. I have to compose a cache to avoid returning wrong data.
                string cacheKey = GetCacheKeyForProductsQuery(qp);

                var entities = await _cache.GetOrCreateAsync(cacheKey, (entry) => {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                    return _productsRepository.ListPaginationAsync(qp);
                });

                var models = entities != null && entities.Any()
                    ? _mapper.Map(entities, new List<ProductServiceResult>())
                    : new List<ProductServiceResult>();

                return models;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<ProductServiceResult> GetProductAsync(string id)
        {
            try
            {
                var entity = await _productsRepository.FindByIdAsReadableAsync(id);
                var model = entity != null
                    ? _mapper.Map(entity, new ProductServiceResult())
                    : null;
                return model;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<int> GetProductsCountAsync()
        {
            try
            {
                return await _productsRepository.CountAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                throw;
            }
        }

        private string GetCacheKeyForProductsQuery(ProductsQuery query)
        {
            string key = CacheKeys.ProductsList.ToString();

            if (query.CategoryId.HasValue && query.CategoryId > 0)
            {
                key = string.Concat(key, "_", query.CategoryId.Value);
            }

            if (query.Price.HasValue && query.Price > 0)
            {
                key = string.Concat(key, "_", query.Price.Value);
            }

            if (query.Inventory.HasValue && query.Inventory > 0)
            {
                key = string.Concat(key, "_", query.Inventory.Value);
            }

            if (query.CreatedAtFrom.HasValue || query.CreatedAtTo.HasValue)
            {
                if (query.CreatedAtFrom.HasValue && query.CreatedAtTo.HasValue)
                {
                    key = string.Concat(key, "_", query.CreatedAtFrom.Value, "_", query.CreatedAtTo.Value);
                }
                else if (query.CreatedAtFrom.HasValue)
                {
                    key = string.Concat(key, "_", query.CreatedAtFrom.Value);
                }
                else if (query.CreatedAtTo.HasValue)
                {
                    key = string.Concat(key, "_", query.CreatedAtTo.Value);
                }
            }

            if (!string.IsNullOrEmpty(query.Search))
            {
                key = string.Concat(key, "_", query.Search);
            }

            if (query.Limit.HasValue && query.Offset.HasValue)
            {
                key = string.Concat(key, "_", query.Limit, "_", query.Offset);
            }

            return key;
        }
    }
}
