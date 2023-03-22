using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdersApp.Shared.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Status Status { get; set; }

        [MaxLength(128)]
        public string ClientName { get; set; } = null!;

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [MaxLength(256)]
        public string? AdditionalInfo { get; set; }
        public IList<OrderLine> Lines { get; set; } = null!;
    }
}
