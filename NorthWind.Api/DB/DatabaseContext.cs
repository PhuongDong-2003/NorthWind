using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NorthWind.Core.Entity;


namespace NorthWind.Api.Controllers
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ConnectionStrings DatabaseSetting { get; }

         public DatabaseContext(DbContextOptions<DatabaseContext> options, ConnectionStrings databaseSetting)
            : base(options)
        {
            this.DatabaseSetting = databaseSetting;
        }

      
    }
}