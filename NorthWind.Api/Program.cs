using Microsoft.Data.SqlClient;
using Microsoft.OpenApi.Models;
using NorthWind.Api.Controllers;
using NorthWind.Api.Repository;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Employees", Version = "v2" });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "Employees");
});
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
