using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Utilities.DomainHelpers.Repositories.Contracts
{
    public interface IBaseRepository<T, QueryParamT>
    {
        Task<IEnumerable<T>> ListPaginationAsync(QueryParamT query);
        Task<IEnumerable<T>> ListAsync();
        Task<int> CountAsync();
        Task AddRangeAsync(List<T> entities);
        Task AddAsync(T entity);
        Task<List<T>> FindByIdsAsWritableAsync(IEnumerable<string> ids);
        Task<IEnumerable<T>> FindByIdsAsReadableAsync(IEnumerable<string> ids);
        Task<T> FindByIdAsWritableAsync(string id);
        Task<T> FindByIdAsReadableAsync(string id);
        void Update(T entity);
        void Remove(T entity);
    }
}
