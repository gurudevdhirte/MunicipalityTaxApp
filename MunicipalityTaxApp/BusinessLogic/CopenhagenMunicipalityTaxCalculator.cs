using MunicipalityTaxApp.Controllers;
using MunicipalityTaxApp.Logger;
using System;
using System.Collections.Generic;
using System.Text;

namespace MunicipalityTaxApp.BusinessLogic
{
    public class CopenhagenMunicipalityTaxCalculator : MunicipalityTaxCalculator   {        

        public CopenhagenMunicipalityTaxCalculator(ILogger logger) : base(logger) { }

        public override double GetTax(string MunicipalityName, DateTime inputDate)
        {
            _logger.Log("Calculating Tax for " + MunicipalityName + " for the Date:"+ inputDate.ToShortDateString());

            double taxRate = base.GetTax(MunicipalityName, inputDate)              ;
            return taxRate;
        }
    }
}
