Insert Into Tax Values('Daily')
Insert Into Tax Values('Weekly')
Insert Into Tax Values('Monthly')
Insert Into Tax Values('Yearly')

Select * from Tax

Insert Into Municipality(MunicipalityName) Values('Copenhagen')

Insert Into MunicipalityTaxMapping (MunicipalityId,TaxId,StartDate,EndDate,TaxRate)
Values(1,1,'01/01/2016','01/01/2016',0.1)
Insert Into MunicipalityTaxMapping (MunicipalityId,TaxId,StartDate,EndDate,TaxRate)
Values(1,1,'12/25/2016','12/25/2016',0.1)
Insert Into MunicipalityTaxMapping (MunicipalityId,TaxId,StartDate,EndDate,TaxRate)
Values(1,3,'05/01/2016','05/31/2016',0.4)
Insert Into MunicipalityTaxMapping (MunicipalityId,TaxId,StartDate,EndDate,TaxRate)
Values(1,4,'01/01/2016','12/31/2016',0.2)

Truncate Table Municipality

