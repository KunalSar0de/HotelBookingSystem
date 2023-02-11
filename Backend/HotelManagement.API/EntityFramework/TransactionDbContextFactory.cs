using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HotelManagement.API.src.EntityFramework
{
    public class TransactionDbContextFactory : IDesignTimeDbContextFactory<TransactionContext>
    {
		public TransactionContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

            var optionsBuilder = new DbContextOptionsBuilder();

            var connectionString = configuration.GetConnectionString("TransactionDatabase");
            optionsBuilder.UseMySql(connectionString , x=>x.MigrationsAssembly("HotelManagement.API"));

            return new TransactionContext(optionsBuilder.Options);
        }
        
    }
}