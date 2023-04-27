namespace OrdersApp.Shared.DTO
{
    public class UserSession
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
        public required int ExpireIn { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required string Token { get; set; }
    }
}
