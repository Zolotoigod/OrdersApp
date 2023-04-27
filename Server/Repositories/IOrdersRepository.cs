using OrdersApp.Shared.DTO;
using OrdersApp.Shared.Models;

namespace OrdersApp.Server.Repositories
{
    public interface IOrdersRepository
    {
        Task AddLine(Guid orderId, LineRequest request);
        Task<Guid> Create(Order order);
        Task DeleteById(Guid id);
        Task<IList<Order>> Find(FilterRequest request);
        Task<Order> ReadById(Guid id);
        Task RemoveLine(Guid lineId);
        Task SetStatus(Guid id, Status newStasus);
        Task UpdateById(Guid id, UpdateRequest request);
        Task<int> GetCount(FilterRequest request);
    }
}