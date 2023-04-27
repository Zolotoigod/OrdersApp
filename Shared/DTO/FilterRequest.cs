using OrdersApp.Shared.Models;

namespace OrdersApp.Shared.DTO
{
    public class FilterRequest
    {
        public int Skip { get; set; }

        public int Take { get; set; }

        public Status? StatusFilter { get; set; }

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public string? ClientNameFilter { get; set; }
    }
}
