using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OrdersApp.Client;
using OrdersApp.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetSection("ApiUrl").Value!) })
    .AddSingleton<IApiClientService, ApiClientService>();


await builder.Build().RunAsync();
