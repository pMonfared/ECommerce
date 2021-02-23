namespace ECommerce.Api.Products.Presentation.MapperProfiles
{
    public class ProductProfiler : AutoMapper.Profile
    {
        public ProductProfiler()
        {
            CreateMap<Domain.Entities.Product, Presentation.Models.ProductModels.ServiceResults.ProductServiceResult>();
        }
    }
}
