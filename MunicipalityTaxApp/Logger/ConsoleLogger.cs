using System;
using System.Collections.Generic;
using System.Text;

namespace MunicipalityTaxApp.Logger
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
