using System.ComponentModel.DataAnnotations;

namespace OrdersApp.Shared.DTO
{
    public class LineRequest
    {
        [Required]
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
