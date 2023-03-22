using Microsoft.EntityFrameworkCore;
using OrdersApp.Server.Repositories;
using OrdersApp.Server.Servises;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
const string CorsPolicy = "allowAll";
// Add services to the container.

builder.Services.AddDbContext<OrdersAppContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("OrdersDb")), ServiceLifetime.Singleton);

builder.Services
    .AddSingleton<IOrdersRepository, OrdersRepository>()
    .AddSingleton<IOrderService, OrderService>();

builder.Services.AddCors(opt => opt.AddPolicy(CorsPolicy, builder => 
{
    builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
}));

builder.Services.AddControllersWithViews()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseCors(CorsPolicy);
app.UseMiddleware<ExceptionLoggingMiddleware>();

// app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
