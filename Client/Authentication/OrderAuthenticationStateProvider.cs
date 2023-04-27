using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using OrdersApp.Client.Extensions;
using OrdersApp.Shared.DTO;
using System.Security.Claims;

namespace OrdersApp.Client.Autentification
{
    public class OrderAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService sessionStorageService;
        private ClaimsPrincipal anonymus = new ClaimsPrincipal(new ClaimsIdentity());

        public OrderAuthenticationStateProvider(ISessionStorageService sessionStorageService)
        {
            this.sessionStorageService = sessionStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var session = await sessionStorageService.ReadItemIncryptAsync<UserSession>("UserSession");
                if (session == null)
                {
                    return await Task.FromResult(new AuthenticationState(anonymus));
                }

                var claims = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, session.FirstName + " " + session.LastName),
                    new Claim(ClaimTypes.Role, session.Role)
                }, "JwtAuth"));

                return await Task.FromResult(new AuthenticationState(claims));
            }
            catch (Exception)
            {
                return await Task.FromResult(new AuthenticationState(anonymus));
            }
        }

        public async Task UpdateAuthenticationState(UserSession? session)
        {
            ClaimsPrincipal claims;

            if (session is not null)
            {
                claims = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, session.FirstName + " " + session.LastName),
                    new Claim(ClaimTypes.Role, session.Role)
                }));

                await sessionStorageService.SaveItemIncryptAsync("UserSession", session);
            }
            else
            {
                claims = anonymus;
                await sessionStorageService.RemoveItemAsync("UserSession");
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claims)));
        }

        public async Task<string> GetToken()
        {
            var result = string.Empty;

            try
            {
                var session = await sessionStorageService.ReadItemIncryptAsync<UserSession>("UserSession");
                var s = DateTime.UtcNow;
                var d = session?.CreatedAt.AddDays(session.ExpireIn);

                if (session != null & DateTime.UtcNow < session?.CreatedAt.AddDays(session.ExpireIn))
                {
                    result = session?.Token;
                }

                return result!;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
