using Blazored.SessionStorage;
using System.Text;
using System.Text.Json;

namespace OrdersApp.Client.Extensions
{
    public static class SessionsStorageExtensions
    {
        public static async Task SaveItemIncryptAsync<T>(this ISessionStorageService sessionStorage, string key, T item)
        {
            var json = JsonSerializer.Serialize(item);
            var base64Stirng = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
            await sessionStorage.SetItemAsync(key, base64Stirng);
        }

        public static async Task<T> ReadItemIncryptAsync<T> (this ISessionStorageService sessionStorage, string key)
        {
            var base64Stirng = await sessionStorage.GetItemAsync<string>(key);
            var item = JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(Convert.FromBase64String(base64Stirng)));

            return item;
        }
    }
}
