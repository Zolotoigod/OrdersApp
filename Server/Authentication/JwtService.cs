using Microsoft.IdentityModel.Tokens;
using OrdersApp.Shared.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrdersApp.Server.Autentification
{
    public class JwtService
    {
        public const string Key = "dOzl6o-N8Fg3Cu3I9JK2u2EjYz_dGaImcJOt4OfTYKc";
        private const int ExpireTime = 10;

        private readonly IAccauntService service;

        public JwtService(IAccauntService service)
        {
            this.service = service;
        }

        public async Task<UserSession?> CreateToken(string email, string password)
        {
            var acc = await service.GetAccauntByEmail(email);
            if(acc.Password != password)
            {
                return null;
            }

            // Generate Jwt token
            var expiryTime = DateTime.UtcNow.AddMinutes(ExpireTime);
            var tokenKey = Encoding.ASCII.GetBytes(Key);

            var claims = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Name, string.Join(" ", acc.FirstName, acc.LastName)),
                new Claim(ClaimTypes.Email, acc.Email),
                new Claim(ClaimTypes.Role, acc.Role)
            });

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);

            var securityDiscriptor = new SecurityTokenDescriptor()
            {
                Subject = claims,
                Expires = expiryTime,
                SigningCredentials = credentials
            };

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtTokenHandler.CreateToken(securityDiscriptor);
            var token = jwtTokenHandler.WriteToken(securityToken);

            var session = new UserSession()
            {
               FirstName = acc.FirstName,
               LastName = acc.LastName,
               CreatedAt = acc.CreatedAt,
               Email = acc.Email,
               Role = acc.Role,
               ExpireIn = ExpireTime,
               Token = token
            };

            return session;
        }
    }
}
