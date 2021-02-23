using ECommerce.Api.Orders.Domain.Entities;
using ECommerce.Api.Orders.Domain.QueryModels.OrderQueryModels.QueryParams;
using ECommerce.Api.Orders.Domain.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Utilities.DomainHelpers;
using ECommerce.Utilities.DomainHelpers.Repositories;

namespace ECommerce.Api.Orders.Domain.Repositories
{
    public class OrdersRepositoryProvider : BaseRepository, IOrdersRepository
    {
        private readonly DbSet<Order> _orders;
        private readonly ILogger<OrdersRepositoryProvider> _logger;

        public OrdersRepositoryProvider(
            IUnitOfWork uow,
            ILogger<OrdersRepositoryProvider> logger) : base(uow)
        {

            _orders = _uow.Set<Order>();
            _logger = logger;
        }

        public async Task AddAsync(Order entity)
        {
            await _orders.AddAsync(entity);
        }

        public async Task<int> CountAsync()
        {
            return await _orders.AsNoTracking().CountAsync();
        }

        public async Task AddRangeAsync(List<Order> entities)
        {
            await _orders.AddRangeAsync(entities);
        }

        public async Task<Order> FindByIdAsReadableAsync(string id)
        {
            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
            return await _orders.AsNoTracking().Include(p=>p.Items)
                                 .FirstOrDefaultAsync(p => p.Id.ToString() == id);
        }

        public async Task<Order> FindByIdAsWritableAsync(string id)
        {
            return await _orders.Include(p=>p.Items)
                                 .FirstOrDefaultAsync(p => p.Id.ToString() == id);
        }

        public async Task<IEnumerable<Order>> FindByIdsAsReadableAsync(IEnumerable<string> ids)
        {
            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
            return await _orders.AsNoTracking().Include(p=>p.Items).Where(p => ids.Contains(p.Id.ToString()))
                                 .ToListAsync();
        }

        public async Task<List<Order>> FindByIdsAsWritableAsync(IEnumerable<string> ids)
        {
            return await _orders.Include(p=>p.Items).Where(p => ids.Contains(p.Id.ToString()))
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Order>> ListPaginationAsync(OrdersQuery query)
        {
            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
            IQueryable<Order> queryable = _orders.Include(p=>p.Items).AsNoTracking();


            if(query.CustomerId <= 0)
                throw new ArgumentOutOfRangeException(nameof(query.CustomerId),$"The {nameof(query.CustomerId)} can not be null");

            queryable = queryable.Where(p => p.CustomerId.Equals(query.CustomerId));

            // Here I apply a simple calculation to skip a given number of items, according to the current page and amount of items per page,
            // and them I return only the amount of desired items. The methods "Skip" and "Take" do the trick here.
            if (query.Limit.HasValue && query.Offset.HasValue)
            {
                queryable = queryable.Skip(query.Offset.Value)
                    .Take(query.Limit.Value);
            }
            
            return await queryable.ToListAsync();
        }

        public async Task<IEnumerable<Order>> ListAsync()
        {
            return await _orders.Include(p=>p.Items).AsNoTracking().ToListAsync();
        }

        public async Task<int> CountAsync(int customerId)
        {
            return await _orders.AsNoTracking().CountAsync(p=> p.CustomerId.Equals(customerId));
        }

        public void Remove(Order entity)
        {
            _orders.Remove(entity);
        }

        public void Update(Order entity)
        {
            _orders.Update(entity);
        }



    }
}
