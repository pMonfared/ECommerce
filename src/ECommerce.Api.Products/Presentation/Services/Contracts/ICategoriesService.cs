using ECommerce.Api.Products.Domain.QueryModels.CategoryModels.QueryParams;
using ECommerce.Api.Products.Presentation.Models.CategoryModels.ServiceResults;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Presentation.Services.Contracts
{
    public interface ICategoriesService
    {
        Task<IEnumerable<CategoryServiceResult>> GetCategoriesAsync(CategoriesQuery qp);
        Task<int> GetCategoriesCountAsync();
        Task<CategoryServiceResult> GetCategoryAsync(string id);
    }
}
