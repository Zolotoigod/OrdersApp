using OrdersApp.Server.Repositories;
using OrdersApp.Shared;
using OrdersApp.Shared.DTO;
using OrdersApp.Shared.Models;

namespace OrdersApp.Server.Servises
{
    public class OrderService : IOrderService
    {
        private readonly IOrdersRepository repository;
        private readonly ILogger<OrderService> logger;

        public OrderService(IOrdersRepository repository, ILogger<OrderService> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<Guid> Create(OrderRequest request)
        {
            var newOrder = new Order()
            {
                CreatedAt = DateTime.UtcNow,
                Status = Status.New,
                ClientName = request.ClientName,
                Price = request.Lines.Sum(l => l.Price),
                AdditionalInfo = request.AdditionalInfo,
                Lines = request.Lines
                    .Select(l => l.ToEntity())
                    .ToList(),
            };

            try
            {
                return await repository.Create(newOrder);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<OrderResponse?> ReadById(Guid id)
        {
            try
            {
                var order = await repository.ReadById(id);
                return order.ToResponse();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<IList<OrderResponse>> Find(FilterRequest request)
        {
            try
            {
                var orders = await repository.Find(request);
                return orders.Select(o => o.ToResponse()).ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task UpdateById(Guid id, UpdateRequest request)
        {
            try
            {
                await repository.UpdateById(id, request);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task DeleteById(Guid id)
        {
            try
            {
                await repository.DeleteById(id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task SetStatus(Guid id, Status newStasus)
        {
            try
            {
                await repository.SetStatus(id, newStasus);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task AddLine(Guid orderId, LineRequest request)
        {
            try
            {
                await repository.AddLine(orderId, request);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task RemoveLine(Guid lineId)
        {
            try
            {
                await repository.RemoveLine(lineId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
