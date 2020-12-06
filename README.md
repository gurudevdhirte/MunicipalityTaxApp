appsettings.json file:
-----------------------

SQLConnection is used to connect from mdf file of the solution in App_Data folder
Separator is used to split the import data of the text file ImportData.txt
ImportData.txt is placed in import folder of the solution and is specified in ImportPath in the appsettings.json file.
Mode is used to indicate whether we want to import the data from text file or get the municiplaity tax for a specific municipality name and a date.
Mode Import is used when we want to import the data from text file.
Mode GetTax is used when we want to get the municiplaity tax for a specific municipality name and a date from the database.
DateFormat key is used to specify the start date and end date format in the ImportData.txt file.
LineFieldsLength key is used to specify the valid field values in text file.

Program.cs
-----------------------

All the dependencies of a class/interface is specified using AddSingleton method.
For the specified mode in the appsettings.json file, Main method will start and either import the data from the text file or Get the tax data from the database.

Database Details:
-----------------------
There are 3 tables used for managing the municipality data i.e. Tax, Municipality and MunicipalityTaxMapping.

Tax will contain the values of Daily, Weekly, Monthly and Yearly.

Municipality table contains an initial value of Copenhagen.

MunicipalityTaxMapping will contain the mapping for any municipality for any tax type  along with start date, end date and taxrate.

MunicipalityTax.mdf file is placed in App_Data folder in the application.

Scripts folder contains the database queries for the mdf file.
InsertScript is executed for setting up the initial data and getting the tax information.

Getting Tax Data for a specific Municipality Name and Date:
---------------------------------------------------------------------
User need to specify the municipality name and date in the console.
For a specific Municiplaity, the corresponding Taxcalculator class will be called.

GetTaxByMunicipalityNameAndDate method is called from the TaxController and will get the data from database for the entered municipality name and date.

For Municiplaity Name : Copenhagen and date : 2016.01.01, the tax rate will be displayed as 0.1.
For Municiplaity Name : Copenhagen and date : 2016.05.02, the tax rate will be displayed as 0.4.

After importing the ImportData.txt
For Municiplaity Name : California and date : 2016.01.01, the tax rate will be displayed as 0.1.
For Municiplaity Name : California and date : 2016.05.02, the tax rate will be displayed as 0.4.

For Municiplaity Name : Copenhagen and date : 2016.04.02, the tax rate will be displayed as 0.35.
For Municiplaity Name : California and date : 2016.04.02, the tax rate will be displayed as 0.3.


Importing Municipality data from Text file:
----------------------------------------------
Application can import the data from the text file with a separator.
ImportDataFromTextFile method is used for inserting validated line from the text file.
If a new municipality name is mentioned in text file, it will be entered in Municipality table and then in MunicipalityTaxMapping table.

California|Yearly|2016.01.01|2016.12.31|0.2|0.123
California|Yearly|2016.01.01|2016.12.31|0.2 
California|Monthly|2016.05.01|2016.05.31|0.4 
California|Daily|2016.01.01|2016.01.01|0.1 
California|Daily|2016.12.25|2016.12.25|0.1 
California|Biweekly|2016.12.25|2016.12.25|0.25
California|Weekly|2016.04.01|2016.04.07|0.3
Copenhagen|Weekly|2016.04.01|2016.04.07|0.35

From the above 8 lines, line 1 and 6 are not saved as line 1 contains more than 5 fields and line 6 contains invalid tax type(BiWeekly) not mentioned in Tax table.

Validating Text file for Importing Municipality data
-----------------------------------------------------
ParseInputFileLine method is used to parse the lines of text file and is placed in FileParser class.
Only validated lines will be saved in database and rest lines will be ignored.

Validated line should meet the below criteria:
1. Valid line should contain only valid tax type like Daily, Weekly, Monthly and Yearly saved in Tax table.
2. Valid line should contain valid datetime format for start date and end date which is validated by ValidateDate method.
3. Valid line should contain taxRate should be in decimal places.
4. Valid line should contain 5 fields as mentioned in LineFieldsLength key in appsettings.json file.

ValidateDate method is used to validate the datetime format specified in the text file.

If a taxmapping is already present and will not be added to the database.

Models used in the application:
------------------------------

TaxFactory class is used to create the object of respective CaliforniaMunicipalityTaxCalculator or CopenhagenMunicipalityTaxCalculator class from reflection based on the user input from console.

TaxEngine class will be called from main method of Program class and will either import the text file data or get the tax from database. It will also get the use input from console.

TaxContext class is used to create connection string based on SQLConnection key in appsettings.json file.
Connection string is specified in OnConfiguring method.

ITaxEngine interface is used indicate the methods required for tax engine and implemented in TaxEngine class.

Tax, Municipality and MunicipalityTaxMapping classes are used to map the Tax, Municipality and MunicipalityTaxMapping tables.

FileParser class is used to parse the text file for importing municipality data.

ConsoleLogger class is used to write a message to console and implements ILogger interface.

BusinessLogic folder contains the tax calculation logic for the different municipalities.
MunicipalityTaxCalculator class implements IMunicipalityTaxCalculator interface and contains the base implementation for getting the tax information from the database.

CaliforniaMunicipalityTaxCalculator class will be used to get the tax rate for California municipality date for an entered date.

CopenhagenMunicipalityTaxCalculator class will be used to get the tax rate for Copenhagen municipality date for an entered date.

Custom logic can be added if required to apply any special tax calculation logic in the respective MunicipalityTaxCalculator class.

Assumptions: 
-------------

User will enter a single Municipality Name and date for getting the tax rate.

























