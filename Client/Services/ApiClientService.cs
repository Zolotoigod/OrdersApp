using OrdersApp.Shared;
using OrdersApp.Shared.DTO;
using OrdersApp.Shared.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OrdersApp.Client.Services
{
    public class ApiClientService : IApiClientService
    {
        private readonly HttpClient client;
        private readonly JsonSerializerOptions jsonOptions;

        private const string BaseApi = "/api/orders";

        public ApiClientService(HttpClient client)
        {
            this.client = client;
            jsonOptions = new JsonSerializerOptions();
            jsonOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            jsonOptions.Converters
                .Add(new JsonStringEnumConverter());
        }

        public async Task<Guid> CreateOrder(OrderRequest request, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await client.PostAsJsonAsync(BaseApi, request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<Guid>();
            return result;
        }

        public async Task<OrderResponse> GetOrder(Guid id, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await client.GetAsync($"{BaseApi}/{id}");
            response!.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<OrderResponse>(jsonOptions);
            return result!;
        }

        public async Task<IList<OrderResponse>> FindOrders(FilterRequestView filter, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var request = new FilterRequest()
            {
                Skip = filter.Skip,
                Take = filter.Take,
                ClientNameFilter = filter.ClientName,
                StatusFilter = filter.Status!.Value.ToEntity(),
                From = filter.From,
                To = filter.To,
            };
            var response = await client.GetAsync($"{BaseApi}{ToQueryString(request)}");
            response.EnsureSuccessStatusCode();
            var s = await response.Content.ReadAsStringAsync();

            var result = await response.Content.ReadFromJsonAsync<List<OrderResponse>>(jsonOptions);
            return result!;
        }

        public async Task UpdateOrder(Guid id, UpdateRequest request, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await client.PutAsJsonAsync($"{BaseApi}/{id}", request);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteOrder(Guid id, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await client.DeleteAsync($"{BaseApi}/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task SetOrderStatus(Guid id, Status newStatus, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await client.PutAsync($"{BaseApi}/status/{id}/{newStatus}", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task AddOrderLine(Guid orderId, LineRequest request, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await client.PostAsJsonAsync($"{BaseApi}/line/{orderId}", request);
            response.EnsureSuccessStatusCode();
        }

        public async Task RemoveOrderLine(Guid lineId, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var response = await client.DeleteAsync($"{BaseApi}/line/{lineId}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<int> GetCount(FilterRequestView filter, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var request = new FilterRequest()
            {
                Skip = filter.Skip,
                Take = filter.Take,
                ClientNameFilter = filter.ClientName,
                StatusFilter = filter.Status!.Value.ToEntity(),
                From = filter.From,
                To = filter.To,
            };
            var response = await client.GetAsync($"{BaseApi}/count{ToQueryString(request)}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<int>(jsonOptions);
            return result;
        }

        private static string ToQueryString(FilterRequest request)
        {
            string status = request.StatusFilter != null
                ? $"&statusfilter={request.StatusFilter}"
                : string.Empty;

            string from = request.From != null
                ? $"&from={request.From}"
                : string.Empty;

            string to = request.To != null
                ? $"&to={request.To}"
                : string.Empty;

            string clientName = request.ClientNameFilter != null
                ? $"&clientnamefilter={request.ClientNameFilter}"
                : string.Empty;

            return $"?skip={request.Skip}&take={request.Take}" + status + from + to + clientName;
        }
    }
}
