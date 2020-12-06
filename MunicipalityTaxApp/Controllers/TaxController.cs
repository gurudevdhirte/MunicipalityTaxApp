using MunicipalityTaxApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http;
using System.Linq;

namespace MunicipalityTaxApp.Controllers
{
    public class TaxController : ApiController
    {
        [HttpGet]
        public double GetTaxByMunicipalityNameAndDate(string MunicipalityName, DateTime inputDate)
        {
            double taxRate = 0;

            using (var context = new TaxContext())
            {
                var query = from MTM in context.MunicipalityTaxMapping
                        join M in context.Municipality on MTM.MunicipalityId equals M.MunicipalityId
                        join T in context.Tax on MTM.TaxId equals T.TaxId
                        where M.MunicipalityName ==MunicipalityName && (MTM.StartDate <= inputDate && MTM.EndDate >= inputDate)
                        select new
                        {
                            TaxId= MTM.TaxId,
                            TaxRate = MTM.TaxRate,
                            StartDate = MTM.StartDate,
                            EndDate = MTM.EndDate
                        };                

                if (query.Count() > 0)
                {
                    taxRate = query.OrderBy(x => x.TaxId).FirstOrDefault().TaxRate;
                }
            }           

            return taxRate;
        }

        [HttpGet]
        public List<Tax> GetTaxes()
        {
            List<Tax> lstTaxes = new List<Tax>();

            using (var context = new TaxContext())
            {
               lstTaxes = (from T in context.Tax
                            select new Tax
                            {
                                TaxId = T.TaxId,
                                TaxName = T.TaxName
                            }).ToList();
            }

            return lstTaxes;
        }

        public List<Municipality> GetMunicipality()
        {
            List<Municipality> lstMunicipality = new List<Municipality>();

            using (var context = new TaxContext())
            {
                lstMunicipality = (from M in context.Municipality
                            select new Municipality
                            {
                                MunicipalityId = M.MunicipalityId,
                                MunicipalityName = M.MunicipalityName
                            }).ToList();
            }

            return lstMunicipality;
        }

        public void ImportDataFromTextFile(string[] municipalityLine)//, string separator
        {           
            string municipalityName = municipalityLine[0];
            string taxType = municipalityLine[1];
            DateTime startDate = Convert.ToDateTime(municipalityLine[2]);
            DateTime endDate = Convert.ToDateTime(municipalityLine[3]);
            double taxRate = Convert.ToDouble(municipalityLine[4]);
            int taxId = 0;
            int municipalityId = 0;

            using (var context = new TaxContext())
            {
                //bool IsMunicipalityExist = context.Municipality.Any(x => x.MunicipalityName.ToLower() == municipalityName);    
                bool IsMunicipalityExist = GetMunicipality().Any(x => x.MunicipalityName == municipalityName);

                if (!IsMunicipalityExist)
                {
                    ImportMunicipality(context, municipalityName);
                    Console.WriteLine("Municipality {0} added ", municipalityName);
                }
                    
                taxId = context.Tax.FirstOrDefault(x => x.TaxName == taxType).TaxId;
                municipalityId = context.Municipality.FirstOrDefault(x => x.MunicipalityName == municipalityName).MunicipalityId;

                bool IsMunicipalityMappingExist = context.MunicipalityTaxMapping.Any(x => x.TaxId == taxId &&
                    x.MunicipalityId == municipalityId && x.StartDate == startDate && x.EndDate == endDate && x.TaxRate == taxRate);

                if (!IsMunicipalityMappingExist)
                {
                    ImportMunicipalityTaxMapping (context, taxId, municipalityId, startDate, endDate, taxRate);
                    Console.WriteLine("Municipality mapping added for municipality name : {0} and tax type {1} ", municipalityName,taxType);
                }
                else
                {
                    Console.WriteLine("Municipality mapping already exist");
                }
            }
        }

        public bool CheckMunicipalityExist(string municipalityName)
        {
            bool isExist = false;

            using (var context = new TaxContext())
            {
                isExist = context.Municipality.Any(x => x.MunicipalityName.ToLower() == municipalityName);
            }

            return isExist;

        }

        private int ImportMunicipality(TaxContext context, string municipalityName)
        {
            Municipality objMunicipality = new Municipality();
            objMunicipality.MunicipalityName = municipalityName;

            var municipality = context.Municipality.Add(objMunicipality);
            return context.SaveChanges();
        }

        private int ImportMunicipalityTaxMapping(TaxContext context, int taxId, int municipalityId, DateTime startDate, DateTime endDate, double taxRate)
        {
            MunicipalityTaxMapping objMTMapping = new MunicipalityTaxMapping();
            objMTMapping.TaxId = taxId;
            objMTMapping.MunicipalityId = municipalityId;
            objMTMapping.StartDate = startDate;
            objMTMapping.EndDate = endDate;
            objMTMapping.TaxRate = taxRate;

            var municipalityTaxMapping = context.MunicipalityTaxMapping.Add(objMTMapping);
            return context.SaveChanges();

        }

        
    }
}
