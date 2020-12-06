using MunicipalityTaxApp.BusinessLogic;
using MunicipalityTaxApp.Logger;
using System;
using System.Collections.Generic;
using System.Text;

namespace MunicipalityTaxApp.Models
{
    public class TaxFactory
    {       
        public MunicipalityTaxCalculator Create(string municipalityName, ILogger logger)
        {
            try
            {
                return (MunicipalityTaxCalculator)Activator.CreateInstance(
                    Type.GetType($"MunicipalityTaxApp.BusinessLogic.{municipalityName}MunicipalityTaxCalculator"),
                    new object[] { logger });                
            }
            catch(Exception ex)
            {
                logger.Log(ex.Message);
                return null;
            }
        }
    }
}
