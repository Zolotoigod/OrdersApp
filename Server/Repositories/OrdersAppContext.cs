using Microsoft.EntityFrameworkCore;
using OrdersApp.Shared.Models;

namespace OrdersApp.Server.Repositories
{
    public class OrdersAppContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderLine> OrdersLine { get; set; }

        public OrdersAppContext(DbContextOptions<OrdersAppContext> options)
            : base(options)
        {
        }
    }
}
