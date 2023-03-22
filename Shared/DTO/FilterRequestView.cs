using OrdersApp.Shared.Models;

namespace OrdersApp.Shared.DTO
{
    public class FilterRequestView
    {
        public int Skip { get; set; }

        public int Take { get; set; }

        public StatusView? Status { get; set; }

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public string? ClientName { get; set; }
    }
}
