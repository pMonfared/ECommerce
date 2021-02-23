using System.Threading.Tasks;

namespace ECommerce.Api.Search.Presentation.Services.Contracts
{
    public interface ISearchService
    {
        Task<dynamic> SearchAsync(int customerId);
    }
}