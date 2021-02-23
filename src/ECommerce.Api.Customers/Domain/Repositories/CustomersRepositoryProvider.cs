using ECommerce.Api.Customers.Domain;
using ECommerce.Api.Customers.Domain.Entities;
using ECommerce.Api.Customers.Domain.QueryModels.CustomerQueryModels.QueryParams;
using ECommerce.Api.Customers.Domain.Repositories;
using ECommerce.Api.Customers.Domain.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Utilities.DomainHelpers;
using ECommerce.Utilities.DomainHelpers.Repositories;

namespace ECommerce.Api.Customers.Domain.Repositories
{
    public class CustomersRepositoryProvider : BaseRepository, ICustomersRepository
    {
        private readonly DbSet<Customer> _customers;
        private readonly ILogger<CustomersRepositoryProvider> _logger;

        public CustomersRepositoryProvider(
            IUnitOfWork uow,
            ILogger<CustomersRepositoryProvider> logger) : base(uow)
        {

            _customers = _uow.Set<Customer>();
            _logger = logger;
        }

        public async Task AddAsync(Customer entity)
        {
            await _customers.AddAsync(entity);
        }

        public async Task AddRangeAsync(List<Customer> entities)
        {
            await _customers.AddRangeAsync(entities);
        }

        public async Task<Customer> FindByIdAsReadableAsync(string id)
        {
            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
            return await _customers.AsNoTracking()
                                 .FirstOrDefaultAsync(p => p.Id.ToString() == id);
        }

        public async Task<Customer> FindByIdAsWritableAsync(string id)
        {
            return await _customers
                                 .FirstOrDefaultAsync(p => p.Id.ToString() == id);
        }

        public async Task<IEnumerable<Customer>> FindByIdsAsReadableAsync(IEnumerable<string> ids)
        {
            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
            return await _customers.AsNoTracking().Where(p => ids.Contains(p.Id.ToString()))
                                 .ToListAsync();
        }

        public async Task<List<Customer>> FindByIdsAsWritableAsync(IEnumerable<string> ids)
        {
            return await _customers.Where(p => ids.Contains(p.Id.ToString()))
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Customer>> ListPaginationAsync(CustomersQuery query)
        {
            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
            IQueryable<Customer> queryable = _customers
                                                    .AsNoTracking();


            if (!string.IsNullOrEmpty(query.Search))
            {
                queryable = queryable.Where(p => 
                    (p.FirstName + " " +
                     p.LastName + " " +
                     p.Address + " " +
                     p.PhoneNumber + " " +
                     p.Email).ToLower().Contains(query.Search.ToLower()));
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

        public async Task<IEnumerable<Customer>> ListAsync()
        {
            return await _customers.AsNoTracking().ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _customers.AsNoTracking().CountAsync();
        }

        public void Remove(Customer entity)
        {
            _customers.Remove(entity);
        }

        public void Update(Customer entity)
        {
            _customers.Update(entity);
        }



    }
}
