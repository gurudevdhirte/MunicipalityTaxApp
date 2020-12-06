CREATE TABLE [dbo].[MunicipalityTaxMapping] (
    [MunicipalityTaxMappingId] INT      IDENTITY (1, 1) NOT NULL,
    [MunicipalityId]           INT      NOT NULL,
    [TaxId]                    INT      NOT NULL,
    [StartDate]                DATETIME NOT NULL,
    [EndDate]                  DATETIME NOT NULL,
    [TaxRate] FLOAT NOT NULL, 
    PRIMARY KEY CLUSTERED ([MunicipalityTaxMappingId] ASC),
    CONSTRAINT [FK_MunicipalityTaxMapping_TaxId] FOREIGN KEY ([TaxId]) REFERENCES [dbo].[Tax] ([TaxId]),
    CONSTRAINT [FK_MunicipalityTaxMapping_MunicipalityId] FOREIGN KEY ([MunicipalityId]) REFERENCES [dbo].[Municipality] ([MunicipalityId])
);

