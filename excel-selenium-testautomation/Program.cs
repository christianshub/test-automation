using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;

namespace TestAutomation
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArguments(new List<string>() { "start-maximized" });

                IWebDriver driver = new ChromeDriver(chromeOptions);

                foreach (var excelFile in Parser.GetExcelFiles())
                {
                    try
                    {
                        new DoSteps(driver, excelFile).StartSteps();
                    }
                    catch
                    {
                        // Needs logging
                    }
                }

                Console.WriteLine("\n\nDONE. \n\n");
            }
            catch 
            {
                // Needs logging
            }
        }
    }
}