using NorthWind.Web;
using NorthWind.Web.Controllers;
using NorthWind.Web.Models;
using NorthWind.Web.Service;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
// cách 1 tham khảo trên learn_microsoft
builder.Services.Configure<ApiUrlsConfiguration>(
builder.Configuration.GetSection(ApiUrlsConfiguration.CONFIG_NAME));
builder.Services.AddHttpClient();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<UserController>();


builder.Services.AddScoped<Print>();
builder.Services.AddScoped<B>();

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
