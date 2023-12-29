using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NorthWind.Api.Controllers;
using NorthWind.Api.Repository;
using Serilog;
using Serilog.Filters;
using Serilog.Formatting.Compact;

Serilog.Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Application", "Northwind.Api")
    .Filter.ByExcluding(le => Matching.FromSource("System")(le))
    .Filter.ByIncludingOnly(le => {
        if (Matching.FromSource("Microsoft")(le))
        {
            return false;
        }
        return true;
    })
    .WriteTo.Console(
        outputTemplate: "{Application} | {Timestamp:HH:mm:ss} | {Name} | {Level} | {SourceContext} | {Message:lj} | {Data} {NewLine}{Exception}")
    .WriteTo.File(new CompactJsonFormatter(),"log.txt")
    .CreateLogger();



var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
ConnectionStrings databaseSetting = new ConnectionStrings();
builder.Configuration.GetSection("ConnectionStrings").Bind(databaseSetting);
builder.Services.AddSingleton(databaseSetting);
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(databaseSetting.Connection));
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();

builder.Services.AddLogging(l =>
{
    l.ClearProviders();
    l.AddSerilog();
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:ValidAudience"],
        ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("admin"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NorthWindApi", Version = "v1" });

    // Cấu hình để sử dụng JWT cho xác thực
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();
app.UseAuthentication();

app.Use(async (context, next) =>
{
    try {
        await next.Invoke();
    } catch (Exception e) {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync("{'err': sad}");
        context.Response.Body.Close();
    }
});

app.Use(async (context, next) =>
{
    var l = context.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("Middleware");
    using var scope = Serilog.Context.LogContext.PushProperty("Name", context.User.Identity.Name);
    await next.Invoke();
    l.LogInformation("Request completed");
    // Do logging or other work that doesn't write to the Response.
});

app.UseAuthorization();
app.MapControllers();

var f = app.Services.GetRequiredService<ILoggerFactory>();
var l = f.CreateLogger("Program");
l.LogInformation("Beginnnnn {country}", "VIETNAM");
app.Run();
