using System;
using System.Collections.Generic;
using System.Text;

namespace MunicipalityTaxApp.BusinessLogic
{
    public interface IMunicipalityTaxCalculator
    {
        public double GetTax(string MunicipalityName, DateTime Date);
    }
}
