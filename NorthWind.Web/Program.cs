using NorthWind.Web;
using NorthWind.Web.Controllers;
using NorthWind.Web.Models;
using NorthWind.Web.Service;
using Serilog;
using Serilog.Filters;
using Serilog.Formatting.Compact;

Serilog.Log.Logger = new LoggerConfiguration()
    .Enrich.WithProperty("Application", "Northwind.Web")
    .Filter.ByExcluding(le => Matching.FromSource("System")(le))
    // .Filter.ByIncludingOnly(le => {
    //     if (Matching.FromSource("Microsoft")(le))
    //     {
    //         return false;
    //     }
    //     return true;
    // })
    .WriteTo.Console(
        outputTemplate: "{Application} | {Timestamp:HH:mm:ss} | {Level} | {SourceContext} | {Message:lj}{NewLine}{Exception}")
    .WriteTo.File(new CompactJsonFormatter(),"log.txt")
    .CreateLogger();
var builder = WebApplication.CreateBuilder(args); 
// Add services to the container.
builder.Services.AddControllersWithViews();
// cách 1 tham khảo trên learn_microsoft
builder.Services.Configure<ApiUrlsConfiguration>(
builder.Configuration.GetSection(ApiUrlsConfiguration.CONFIG_NAME));
builder.Services.AddHttpClient();
builder.Services.AddScoped<ITokenProvider,TokenProvider>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<EmployeeController>();
builder.Services.AddScoped<Print>();
builder.Services.AddScoped<B>();
builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddLogging(l => 
{
    // l.AddConsole();
    // l.AddDebug();
    // l.AddEventSourceLogger();
    // l.AddFilter("Microsoft", LogLevel.Warning);
    // l.AddFilter("System", LogLevel.Warning);
    // l.AddFilter("NorthWind.Web", LogLevel.Information);
    // l.AddFilter("NorthWind.Web.Controllers", LogLevel.Information);
    // l.AddFilter("NorthWind.Web.Service", LogLevel.Information);
    // l.AddFilter("NorthWind.Web.Models", LogLevel.Information);
    // l.AddFilter("NorthWind.Web.Controllers.UserController", LogLevel.Information);
    // l.AddFilter("NorthWind.Web.Controllers.Print", LogLevel.Information);
    // l.AddFilter("NorthWind.Web.Controllers.B", LogLevel.Information);
    // l.AddFilter("NorthWind.Web.Controllers.OrderController", LogLevel.Information);
    // l.AddFilter("NorthWind.Web.Controllers.ProductController", LogLevel.Information); 
    l.ClearProviders();
    l.AddSerilog();

});

// Cách 2 tham khảo project mẫu
// var configSection = builder.Configuration.GetRequiredSection(ApiUrlsConfiguration.CONFIG_NAME);
// builder.Services.Configure<ApiUrlsConfiguration>(configSection);
// var baseUrlConfig = configSection.Get<ApiUrlsConfiguration>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
