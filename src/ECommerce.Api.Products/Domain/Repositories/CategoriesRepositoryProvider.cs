using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Products.Domain.Entities;
using ECommerce.Api.Products.Domain.QueryModels.CategoryModels.QueryParams;
using ECommerce.Api.Products.Domain.Repositories.Contracts;
using ECommerce.Utilities.DomainHelpers;
using ECommerce.Utilities.DomainHelpers.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerce.Api.Products.Domain.Repositories
{
    public class CategoriesRepositoryProvider : BaseRepository, ICategoriesRepository
    {
        private readonly DbSet<Category> _categories;
        private readonly ILogger<CategoriesRepositoryProvider> _logger;

        public CategoriesRepositoryProvider(
            IUnitOfWork uow,
            ILogger<CategoriesRepositoryProvider> logger) : base(uow)
        {

            _categories = _uow.Set<Category>();
            _logger = logger;

        }

        public async Task AddAsync(Category product)
        {
            await _categories.AddAsync(product);
        }

        public async Task AddRangeAsync(List<Category> products)
        {
            await _categories.AddRangeAsync(products);
        }

        public async Task<Category> FindByIdAsReadableAsync(string id)
        {
            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
            return await _categories.AsNoTracking()
                                 .FirstOrDefaultAsync(p => p.Id.ToString() == id);
        }

        public async Task<Category> FindByIdAsWritableAsync(string id)
        {
            return await _categories
                                 .FirstOrDefaultAsync(p => p.Id.ToString() == id);
        }

        public async Task<IEnumerable<Category>> FindByIdsAsReadableAsync(IEnumerable<string> ids)
        {
            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
            return await _categories.AsNoTracking().Where(p => ids.Contains(p.Id.ToString()))
                                 .ToListAsync();
        }

        public async Task<List<Category>> FindByIdsAsWritableAsync(IEnumerable<string> ids)
        {
            return await _categories.Where(p => ids.Contains(p.Id.ToString()))
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Category>> ListPaginationAsync(CategoriesQuery query)
        {
            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
            IQueryable<Category> queryable = _categories
                                                    .AsNoTracking();


            if (!string.IsNullOrEmpty(query.Search))
            {
                queryable = queryable.Where(p =>
                query.Search.Contains(p.Name));
            }


            // Here I apply a simple calculation to skip a given number of items, according to the current page and amount of items per page,
            // and them I return only the amount of desired items. The methods "Skip" and "Take" do the trick here.
            if (query.Limit.HasValue && query.Offset.HasValue)
            {
                queryable = queryable.Skip(query.Offset.Value)
                    .Take(query.Limit.Value);
            }
            
            return await queryable.ToListAsync();
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            return await _categories.AsNoTracking().ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _categories.AsNoTracking().CountAsync();
        }

        public void Remove(Category product)
        {
            _categories.Remove(product);
        }

        public void Update(Category product)
        {
            _categories.Update(product);
        }



    }
}
