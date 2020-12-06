using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MunicipalityTaxApp.BusinessLogic;
using MunicipalityTaxApp.Controllers;
using MunicipalityTaxApp.Logger;
using MunicipalityTaxApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MunicipalityTaxApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var configuration = new ConfigurationBuilder()                
                .AddJsonFile("appsettings.json", false)
                .Build();               

                var serviceProvider = new ServiceCollection()
                        .AddSingleton<ILogger, ConsoleLogger>()
                        .AddSingleton<TaxController, TaxController>()
                        .AddSingleton<IConfiguration>(configuration)
                        .AddSingleton<FileParser, FileParser>()
                        .AddSingleton<ITaxEngine, TaxEngine>()
                        .BuildServiceProvider();
               
                var engine = serviceProvider.GetService<ITaxEngine>();  
                
                string mode = configuration["Mode"];

                if (mode == Mode.Import.ToString())
                {                    
                    engine.ImportMunicipalData();
                }
                else if (mode == Mode.GetTax.ToString())
                {     
                    engine.CalculateTax();
                }               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        public enum Mode
        {
            Import,
            GetTax
        }
    }
}
