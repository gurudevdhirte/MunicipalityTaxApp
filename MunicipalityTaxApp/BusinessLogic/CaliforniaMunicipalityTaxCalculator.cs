using MunicipalityTaxApp.Logger;
using System;
using System.Collections.Generic;
using System.Text;

namespace MunicipalityTaxApp.BusinessLogic
{
    public class CaliforniaMunicipalityTaxCalculator : MunicipalityTaxCalculator
    {        
        public CaliforniaMunicipalityTaxCalculator(ILogger logger) 
            : base(logger){ }

        public override double GetTax(string MunicipalityName, DateTime inputDate)
        {
            _logger.Log("Calculating Tax for " + MunicipalityName + " for the Date:"+ inputDate.ToShortDateString() );           

            double taxRate = base.GetTax(MunicipalityName, inputDate);
            _logger.Log("Additional Custom Logic");
            return taxRate;
        }
    }
}
