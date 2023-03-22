using OrdersApp.Shared.DTO;
using OrdersApp.Shared.Models;

namespace OrdersApp.Server.Servises
{
    public interface IOrderService
    {
        Task AddLine(Guid orderId, LineRequest request);
        Task<Guid> Create(OrderRequest request);
        Task DeleteById(Guid id);
        Task<IList<OrderResponse>> Find(FilterRequest request);
        Task<OrderResponse?> ReadById(Guid id);
        Task RemoveLine(Guid lineId);
        Task SetStatus(Guid id, Status newStasus);
        Task UpdateById(Guid id, UpdateRequest request);
    }
}