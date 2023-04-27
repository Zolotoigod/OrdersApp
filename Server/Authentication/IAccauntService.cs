using OrdersApp.Shared.Models;

namespace OrdersApp.Server.Autentification
{
    public interface IAccauntService
    {
        public Task<Accaunt> GetAccauntByEmail(string email);
    }
}
