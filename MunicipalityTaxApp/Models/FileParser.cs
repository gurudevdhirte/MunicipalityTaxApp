using MunicipalityTaxApp.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace MunicipalityTaxApp.Models
{
    public class FileParser
    {
        private readonly TaxController _taxController;
        private readonly IConfiguration _config;

        public FileParser(TaxController taxController, IConfiguration config)
        {
            _taxController = taxController;
            _config = config;
        }
        public bool ParseInputFileLine(string[] municipalityLine)
        {
            try
            {
                bool isValid = true;
                int lineFieldsLength = Convert.ToInt32(_config["LineFieldsLength"]);


                if (municipalityLine.Length == lineFieldsLength)
                {
                    List<Tax> lstTax = _taxController.GetTaxes();
                    string taxType = string.Empty;
                    string startDate = string.Empty;
                    string endDate = string.Empty;
                    double taxRate = 0;

                    bool isValidTaxType = false;

                    taxType = municipalityLine[1];
                    startDate = municipalityLine[2];
                    endDate = municipalityLine[3];

                    isValidTaxType = lstTax.Any(x => x.TaxName.ToLower() == taxType.ToLower());

                    if (!isValidTaxType)
                    {
                        isValid = false;                        
                        Console.WriteLine("Invalid tax type : " + taxType);
                        return isValid;
                    }

                    if (!ValidateDate(startDate) || !ValidateDate(endDate))
                    {
                        isValid = false;                        
                        Console.WriteLine("Invalid StartDate /Enddate ");
                        return isValid;
                    }

                    if (!double.TryParse(municipalityLine[4], out taxRate))
                    {
                        isValid = false;                        
                        Console.WriteLine("Invalid Tax Rate");
                        return isValid;
                    }
                }
                else
                {
                    isValid = false;                   
                    Console.WriteLine("Invalid Tax line to process");
                    return isValid;
                }
                
                return isValid;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool ValidateDate(string inputDate)
        {
            bool isValidDate = false;
            DateTime date;
            string dateformat = _config["DateFormat"];

            isValidDate = DateTime.TryParseExact(inputDate, dateformat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date);

            return isValidDate;
        }
    }
}
