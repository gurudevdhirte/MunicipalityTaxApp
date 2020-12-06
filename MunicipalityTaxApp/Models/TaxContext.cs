using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MunicipalityTaxApp.Models
{
    public class TaxContext : DbContext
    {       
        private string connectionString;

        public TaxContext(): base()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);
            
            var configuration = builder.Build();

            connectionString = configuration.GetConnectionString("SQLConnection").ToString();
        }
        public DbSet<Tax> Tax { get; set; }
        public DbSet<Municipality> Municipality { get; set; }

        public DbSet<MunicipalityTaxMapping> MunicipalityTaxMapping { get; set; }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {                
                optionsBuilder.UseSqlServer(connectionString);
            }
            catch(Exception ex)
            {
                Console.WriteLine("An error has occured while setting the connection string");
            }
        }
    }
}
