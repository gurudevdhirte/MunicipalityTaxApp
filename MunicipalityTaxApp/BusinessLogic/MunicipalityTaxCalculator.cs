using MunicipalityTaxApp.Controllers;
using MunicipalityTaxApp.Logger;
using System;
using System.Collections.Generic;
using System.Text;

namespace MunicipalityTaxApp.BusinessLogic
{
    public class MunicipalityTaxCalculator : IMunicipalityTaxCalculator
    {        
        public readonly ILogger _logger;
       
        public MunicipalityTaxCalculator(ILogger logger)
        {            
            _logger = logger;          
        }      

        public virtual double GetTax(string MunicipalityName, DateTime inputDate)
        {
            TaxController taxController = new TaxController();
            double taxRate = taxController.GetTaxByMunicipalityNameAndDate(MunicipalityName, inputDate);

            return taxRate;
        }
    }
}
