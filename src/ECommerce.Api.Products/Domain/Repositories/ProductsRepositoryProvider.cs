using ECommerce.Api.Products.Domain.Entities;
using ECommerce.Api.Products.Domain.QueryModels.ProductModels.QueryParams;
using ECommerce.Api.Products.Domain.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Utilities.DomainHelpers;
using ECommerce.Utilities.DomainHelpers.Repositories;

namespace ECommerce.Api.Products.Domain.Repositories
{
    public class ProductsRepositoryProvider : BaseRepository, IProductsRepository
    {
        private readonly DbSet<Product> _products;
        private readonly ILogger<ProductsRepositoryProvider> _logger;

        public ProductsRepositoryProvider(
            IUnitOfWork uow,
            ILogger<ProductsRepositoryProvider> logger) : base(uow)
        {

            _products = _uow.Set<Product>();
            _logger = logger;
        }



        public async Task<IEnumerable<Product>> ListPaginationAsync(ProductsQuery query)
        {
            IQueryable<Product> queryable = _products
                                                    .Include(p => p.Category)
                                                    .AsNoTracking();

            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
            if (query.CategoryId.HasValue && query.CategoryId > 0)
            {
                queryable = queryable.Where(p => p.CategoryId == query.CategoryId);
            }

            if (query.Price.HasValue && query.Price > 0)
            {
                queryable = queryable.Where(p => p.Price == query.Price);
            }

            if (query.Inventory.HasValue && query.Inventory > 0)
            {
                queryable = queryable.Where(p => p.Inventory == query.Inventory);
            }

            if (query.CreatedAtFrom.HasValue || query.CreatedAtTo.HasValue)
            {
                if (query.CreatedAtFrom.HasValue && query.CreatedAtTo.HasValue)
                {
                    queryable = queryable.Where(p => p.CreatedAt >= query.CreatedAtFrom.Value && p.CreatedAt <= query.CreatedAtTo.Value);
                }
                else if (query.CreatedAtFrom.HasValue)
                {
                    queryable = queryable.Where(p => p.CreatedAt >= query.CreatedAtFrom.Value);
                }
                else if (query.CreatedAtTo.HasValue)
                {
                    queryable = queryable.Where(p => p.CreatedAt <= query.CreatedAtTo.Value);
                }
            }

            if (!string.IsNullOrEmpty(query.Search))
            {
                queryable = queryable.Where(p => (p.Name + " " + p.Category.Name).ToLower().Contains(query.Search.ToLower()));
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

        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _products.AsNoTracking()
                                 .Include(p => p.Category)
                                 .ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _products.AddAsync(product);
        }

        public async Task AddRangeAsync(List<Product> products)
        {
            await _products.AddRangeAsync(products);
        }

        public async Task<Product> FindByIdAsReadableAsync(string id)
        {
            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
            return await _products.AsNoTracking()
                                 .Include(p => p.Category)
                                 .FirstOrDefaultAsync(p => p.Id.ToString() == id);
        }

        public async Task<Product> FindByIdAsWritableAsync(string id)
        {
            return await _products
                                 .Include(p => p.Category)
                                 .FirstOrDefaultAsync(p => p.Id.ToString() == id);
        }

        public async Task<IEnumerable<Product>> FindByIdsAsReadableAsync(IEnumerable<string> ids)
        {
            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
            return await _products.AsNoTracking().Where(p => ids.Contains(p.Id.ToString()))
                                 .Include(p => p.Category)
                                 .ToListAsync();
        }

        public async Task<List<Product>> FindByIdsAsWritableAsync(IEnumerable<string> ids)
        {
            return await _products.Where(p => ids.Contains(p.Id.ToString()))
                                 .Include(p => p.Category)
                                 .ToListAsync();
        }


        public async Task<int> CountAsync()
        {
            return await _products.AsNoTracking().CountAsync();
        }

        public void Remove(Product product)
        {
            _products.Remove(product);
        }

        public void Update(Product product)
        {
            _products.Update(product);
        }
    }
}
