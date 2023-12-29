using Microsoft.AspNetCore.Authentication.Cookies;
using NorthWind.Shop.Models;
using NorthWind.Shop.Service;
using NorthWind.Web.Service;
using Serilog;
using Serilog.Filters;
using Serilog.Formatting.Compact;

Serilog.Log.Logger = new LoggerConfiguration()
    .Enrich.WithProperty("Application", "Northwind.Shop")
    .Filter.ByExcluding(le => Matching.FromSource("System")(le))
    .Filter.ByIncludingOnly(le => {
        if (Matching.FromSource("Microsoft")(le))
        {
            return false;
        }
        return true;
    })
    .WriteTo.Console(
        outputTemplate: "{Application} | {Timestamp:HH:mm:ss} | {Level} | {SourceContext} | {Message:lj} {NewLine}{Exception}")
    .WriteTo.File(new CompactJsonFormatter(),"log.txt")
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<ApiUrlsConfiguration>(
builder.Configuration.GetSection(ApiUrlsConfiguration.CONFIG_NAME));
builder.Services.AddHttpClient();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<OrderDetailsService>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();

builder.Services.AddLogging(l =>
{
    l.ClearProviders();
    l.AddSerilog();
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    options.LoginPath = "/Account/CheckLogin";
  
});
builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(20);
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
