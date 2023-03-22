using OrdersApp.Shared.DTO;
using OrdersApp.Shared.Models;

namespace OrdersApp.Client.Services
{
    public interface IApiClientService
    {
        Task AddOrderLine(Guid orderId, LineRequest request);
        Task<Guid> CreateOrder(OrderRequest request);
        Task DeleteOrder(Guid id);
        Task<IList<OrderResponse>> FindOrders(FilterRequestView filter);
        Task<OrderResponse> GetOrder(Guid id);
        Task RemoveOrderLine(Guid lineId);
        Task SetOrderStatus(Guid id, Status newStatus);
        Task UpdateOrder(Guid id, UpdateRequest request);
    }
}