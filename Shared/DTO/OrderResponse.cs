using OrdersApp.Shared.Models;

namespace OrdersApp.Shared.DTO
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Status Status { get; set; }
        public string? ClientName { get; set; }
        public decimal Price { get; set; }
        public string? AdditionalInfo { get; set; }
        public IList<LineResponse>? Lines { get; set; }
    }
}
