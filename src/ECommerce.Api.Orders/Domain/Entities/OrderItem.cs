namespace ECommerce.Api.Orders.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}