using OrdersApp.Shared.Models;

namespace OrdersApp.Server.Autentification
{
    public class AccountService : IAccauntService
    {
        public async Task<Accaunt> GetAccauntByEmail(string email)
        {
            return new Accaunt() 
            { 
                FirstName = "Isaak",
                LastName = "Newton",
                Email = "IsaakNewton@gmail.com",
                Password = "password",
                Role = "Administrator",
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
