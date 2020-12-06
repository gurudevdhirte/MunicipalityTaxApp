using MunicipalityTaxApp.Controllers;
using MunicipalityTaxApp.Logger;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Linq;
using MunicipalityTaxApp.BusinessLogic;
using Microsoft.Extensions.Configuration;

namespace MunicipalityTaxApp.Models
{
    public class TaxEngine: ITaxEngine
    {        
        private readonly ILogger _logger;
        private readonly TaxController _taxController;        
        private readonly IConfiguration _config;
        private readonly FileParser _parser;

        public TaxEngine(ILogger logger,TaxController taxController, IConfiguration config, FileParser parser)
        {
            _logger = logger;
            _taxController = taxController;           
            _config = config;
            _parser = parser;
        }

        public void CalculateTax()
        {
            try
            {
                _logger.Log("Calculating Tax");
                string dateformat = _config["DateFormat"];

                Console.WriteLine("Please enter Municipality Name");
                string municipalityName = Console.ReadLine();
                //string municiplityName = "Copenhagen";
                //string municiplityName = "California";
                _logger.Log("Municipality Name: " + municipalityName);

                bool IsMunicipalityExist = _taxController.GetMunicipality().Any(x => x.MunicipalityName.ToLower() == municipalityName.ToLower());

                if (IsMunicipalityExist)
                {
                    Console.WriteLine("Please enter Date");
                    string date = Console.ReadLine();

                    //string date = "2016.05.02";
                    //string date = "2016.04.01";

                    DateTime inputdate = DateTime.ParseExact(date, dateformat, CultureInfo.InvariantCulture);
                    _logger.Log("Date: " + inputdate.ToShortDateString());

                    var factory = new TaxFactory();

                    var rater = factory.Create(municipalityName, _logger);
                    double? taxRate = rater?.GetTax(municipalityName, inputdate);

                    if (taxRate == 0)
                    {
                        _logger.Log("Tax for " + municipalityName + " for the Date:" + inputdate.ToShortDateString() + " is not present ");
                    }
                    else
                    {
                        _logger.Log("");
                        _logger.Log("Tax for " + municipalityName + " for the Date:" + inputdate.ToShortDateString() + " is " + taxRate);

                        _logger.Log("Tax calculation completed.");
                    }
                }
                else
                {
                    _logger.Log("Municipality Data is not present");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("An error has occured while calculating tax");
            }
        }

        public void ImportMunicipalData()
        {            
            string importFilePath = _config["ImportPath"];
            
            string[] lines = File.ReadAllLines(importFilePath);
            string separator = _config["Separator"];
           
            foreach(string line in lines)
            {
                string[] municipalityLine = line.Split(separator);

                if (_parser.ParseInputFileLine(municipalityLine))
                {                    
                    _taxController.ImportDataFromTextFile(municipalityLine);
                }
            }               
        }
    }
}
