namespace ECommerce.Api.Orders.Presentation.ServiceModels.OrderItemServiceModels.ServiceResults
{
    public class OrderItemServiceResult
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}