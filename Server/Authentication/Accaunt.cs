namespace OrdersApp.Shared.Models
{
    public class Accaunt
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required string Password { get; set; }
    }
}
