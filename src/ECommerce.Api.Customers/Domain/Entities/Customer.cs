using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Api.Customers.Domain.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        [NotMapped]
        public string DisplayName
        {
            get
            {
                var displayName = $"{FirstName} {LastName}";
                return string.IsNullOrWhiteSpace(displayName) ? Email : displayName;
            }
        }
        [StringLength(450)]
        public string FirstName { get; set; }
        [StringLength(450)]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
