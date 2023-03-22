using Microsoft.EntityFrameworkCore;
using OrdersApp.Shared.DTO;
using OrdersApp.Shared.Models;

namespace OrdersApp.Server.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly OrdersAppContext context;

        public OrdersRepository(OrdersAppContext context)
        {
            this.context = context;
        }

        public async Task<Guid> Create(Order order)
        {
            order.Id = Guid.NewGuid();
            await context.OrdersLine.AddRangeAsync(SetIds(order.Lines));
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();

            return order.Id;
        }

        public async Task<Order> ReadById(Guid id) =>
            await context.Orders
            .Include(o => o.Lines)
            .FirstAsync(o => o.Id == id);

        public async Task<IList<Order>> Find(FilterRequest request)
        {
            var from = request.From != null
                ? request.From
                : DateTime.MinValue;

            var to = request.To != null
                ? request.To
                : DateTime.UtcNow;

            var clientName = request.ClientNameFilter != null
                ? request.ClientNameFilter
                : string.Empty;

            if (request.StatusFilter == null)
            {
                return await context.Orders
                .Include(o => o.Lines)
                .Skip(request.Skip)
                .Take(request.Take)
                .Where(
                    o => o.CreatedAt > from
                    & o.CreatedAt < to
                    & o.ClientName.Contains(clientName))
                .OrderBy(o => o.CreatedAt)
                .ToListAsync();
            }
            else
            {
                return await context.Orders
                .Include(o => o.Lines)
                .Skip(request.Skip)
                .Take(request.Take)
                .Where(
                    o => o.CreatedAt > from
                    & o.CreatedAt < to
                    & o.Status == request.StatusFilter
                    & o.ClientName.Contains(clientName))
                .OrderBy(o => o.CreatedAt)
                .ToListAsync();
            }
        }

        public async Task UpdateById(Guid id, UpdateRequest request)
        {
            var order = await context.Orders
                .Include(o => o.Lines)
                .FirstAsync(o => o.Id == id);

            order.ClientName = request.ClientName ?? order.ClientName;
            order.AdditionalInfo = request.AdditionalInfo ?? order.AdditionalInfo;

            await context.SaveChangesAsync();
        }

        public async Task DeleteById(Guid id)
        {
            var order = await context.Orders
                .Include(o => o.Lines)
                .FirstAsync(o => o.Id == id);

            context.OrdersLine.RemoveRange(order.Lines);
            context.Orders.Remove(order);

            await context.SaveChangesAsync();
        }

        public async Task SetStatus(Guid id, Status newStasus)
        {
            var order = await context.Orders
            .Include(o => o.Lines)
            .FirstAsync(o => o.Id == id);

            order.Status = newStasus;

            await context.SaveChangesAsync();
        }

        public async Task AddLine(Guid orderId, LineRequest request)
        {
            var order = await context.Orders
            .Include(o => o.Lines)
            .FirstAsync(o => o.Id == orderId);

            var line = new OrderLine()
            {
                Id = Guid.NewGuid(),
                ProductName = request.ProductName!,
                Price = request.Price,
                OrderId = order.Id,
                Order = order,
            };

            context.Orders.Update(order);
            order.Price += request.Price;

            await context.OrdersLine.AddAsync(line);

            await context.SaveChangesAsync();
        }

        public async Task RemoveLine(Guid lineId)
        {
            var oreder = await context.Orders
                .Include(o => o.Lines)
                .FirstAsync(o => o.Lines.Any(l => l.Id == lineId));

            var line = oreder.Lines.First(l => l.Id == lineId);

            oreder.Price -= line.Price;
            context.OrdersLine.Remove(line);
            await context.SaveChangesAsync();
        }

        private IList<OrderLine> SetIds(IList<OrderLine> orderLines)
        {
            foreach (var line in orderLines)
            {
                line.Id = Guid.NewGuid();
            }

            return orderLines;
        }
    }
}
