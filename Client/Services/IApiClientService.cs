using OrdersApp.Shared.DTO;
using OrdersApp.Shared.Models;

namespace OrdersApp.Client.Services
{
    public interface IApiClientService
    {
        Task AddOrderLine(Guid orderId, LineRequest request, string token);
        Task<Guid> CreateOrder(OrderRequest request, string token);
        Task DeleteOrder(Guid id, string token);
        Task<IList<OrderResponse>> FindOrders(FilterRequestView filter, string token);
        Task<OrderResponse> GetOrder(Guid id, string token);
        Task RemoveOrderLine(Guid lineId, string token);
        Task SetOrderStatus(Guid id, Status newStatus, string token);
        Task UpdateOrder(Guid id, UpdateRequest request, string token);
        Task<int> GetCount(FilterRequestView filter, string token);
    }
}