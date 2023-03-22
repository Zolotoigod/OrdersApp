using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdersApp.Shared.Models
{
    public class OrderLine
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        [MaxLength(128)]
        public string ProductName { get; set; } = null!;

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public Order? Order { get; set; }
    }
}
