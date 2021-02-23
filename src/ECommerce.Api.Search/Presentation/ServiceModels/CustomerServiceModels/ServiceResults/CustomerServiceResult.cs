using System;

namespace ECommerce.Api.Search.Presentation.ServiceModels.CustomerServiceModels.ServiceResults
{
    public class CustomerServiceResult
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
