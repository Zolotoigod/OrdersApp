using System.ComponentModel.DataAnnotations;

namespace OrdersApp.Shared.DTO
{
    public class OrderRequest
    {
        [MaxLength(128)]
        public string ClientName { get; set; } = null!;

        [MaxLength(256)]
        public string? AdditionalInfo { get; set; }
        public IList<LineRequest> Lines { get; set; } = null!;
    }
}
