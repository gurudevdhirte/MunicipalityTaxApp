using System;
using System.Collections.Generic;
using System.Text;

namespace MunicipalityTaxApp.Models
{
    public interface ITaxEngine
    {
        public void CalculateTax();
        public void ImportMunicipalData();
    }
}
