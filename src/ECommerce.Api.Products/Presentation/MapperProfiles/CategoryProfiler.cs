namespace ECommerce.Api.Products.Presentation.MapperProfiles
{
    public class CategoryProfiler : AutoMapper.Profile
    {
        public CategoryProfiler()
        {
            CreateMap<Domain.Entities.Category, Presentation.Models.CategoryModels.ServiceResults.CategoryServiceResult>();
        }
    }
}
