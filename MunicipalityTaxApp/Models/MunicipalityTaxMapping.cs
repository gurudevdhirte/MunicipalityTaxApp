using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MunicipalityTaxApp.Models
{
    public class MunicipalityTaxMapping
    {
        [Key]
        public int MunicipalityTaxMappingId { get; set; }
        public int MunicipalityId { get; set; }
        public int TaxId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TaxRate { get; set; }
    }
}
