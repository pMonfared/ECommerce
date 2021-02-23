using AutoMapper;
using ECommerce.Api.Products.Domain.QueryModels.CategoryModels.QueryParams;
using ECommerce.Api.Products.Domain.Repositories.Contracts;
using ECommerce.Api.Products.Presentation.Services.Contracts;
using ECommerce.Api.Products.Presentation.Models.CategoryModels.ServiceResults;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Products.Presentation.Infrastructure;
using ECommerce.Utilities.DomainHelpers;
using ECommerce.Utilities.PresentationHelpers.Services;

namespace ECommerce.Api.Products.Presentation.Services
{
    public class CategoriesServiceProvider : BaseService, ICategoriesService
    {

        private readonly IMemoryCache _cache;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly ILogger<CategoriesServiceProvider> _logger;
        private readonly IMapper _mapper;

        public CategoriesServiceProvider(
            IUnitOfWork uow,
            IMemoryCache cache,
            ICategoriesRepository categoriesRepository,
            ILogger<CategoriesServiceProvider> logger,
            IMapper mapper) : base(uow)
        {
            _cache = cache;
            _categoriesRepository = categoriesRepository;
            _logger = logger;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }


        public async Task<IEnumerable<CategoryServiceResult>> GetCategoriesAsync(CategoriesQuery qp)
        {
            try
            {
                // Here I list the query result from cache if they exist, but now the data can vary according to the category ID, page and amount of
                // items per page. I have to compose a cache to avoid returning wrong data.
                string cacheKey = GetCacheKeyForCategoriesQuery(qp);

                var entities = await _cache.GetOrCreateAsync(cacheKey, (entry) => {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                    return _categoriesRepository.ListPaginationAsync(qp);
                });

                var models = entities != null && entities.Any()
                    ? _mapper.Map(entities, new List<CategoryServiceResult>())
                    : new List<CategoryServiceResult>();

                return models;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                throw;
            }

        }

        public async Task<CategoryServiceResult> GetCategoryAsync(string id)
        {
            try
            {
                var entity = await _categoriesRepository.FindByIdAsReadableAsync(id);
                var model = entity != null
                    ? _mapper.Map(entity, new CategoryServiceResult())
                    : null;
                return model;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<int> GetCategoriesCountAsync()
        {
            try
            {
                return await _categoriesRepository.CountAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                throw;
            }
        }


        private string GetCacheKeyForCategoriesQuery(CategoriesQuery query)
        {
            string key = CacheKeys.CategoriesList.ToString();

            if (!string.IsNullOrEmpty(query.Search))
            {
                key = string.Concat(key, "_", query.Search);
            }

            key = string.Concat(key, "_", query.Limit, "_", query.Offset);
            return key;
        }
    }
}
