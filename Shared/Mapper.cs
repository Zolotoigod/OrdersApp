using OrdersApp.Shared.DTO;
using OrdersApp.Shared.Models;

namespace OrdersApp.Shared
{
    public static class Mapper
    {
        public static OrderResponse ToResponse(this Order order) =>
            new OrderResponse()
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                Status = order.Status,
                ClientName = order.ClientName,
                Price = order.Price,
                AdditionalInfo = order.AdditionalInfo,
                Lines = order.Lines
                    .Select(ToResponse)
                    .ToList(),
            };

        public static LineResponse ToResponse(this OrderLine line) =>
            new LineResponse()
            {
                Id = line.Id,
                ProductName = line.ProductName,
                Price = line.Price,
            };

        public static OrderLine ToEntity(this LineRequest line) =>
            new OrderLine()
            {
                ProductName = line.ProductName!,
                Price = line.Price,
            };

        public static Status? ToEntity(this StatusView view) => view switch
        {
            StatusView.New => Status.New,
            StatusView.Confirm => Status.Confirm,
            StatusView.Delivery => Status.Delivery,
            StatusView.Cancel => Status.Cancel,
            _ => null,
        };
    }
}
