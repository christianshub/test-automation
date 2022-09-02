using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;

namespace TestAutomation
{
    internal class Helper
    {
        public static bool DetectToasters(IWebDriver driver)
        {
            try
            {
                if (driver.PageSource.Contains("toast-title ng-star-inserted"))
                {
                    // Toaster found
                    Thread.Sleep(500);
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public static void Wait(IWebDriver driver, int seconds, string xpath)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            IWebElement searchResult =
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(xpath)));
        }

        public static void Click(IWebDriver driver, string xpath)
        {
            driver.FindElement(By.XPath(xpath)).Click();
            Thread.Sleep(500);
        }

        public static void WaitClick(IWebDriver driver, int seconds, string xpath)
        {
            Wait(driver, seconds, xpath);
            Click(driver, xpath);
        }

        public static void WaitSend(IWebDriver driver, int seconds, string xpath, string input)
        {
            Wait(driver, seconds, xpath);
            Send(driver, xpath, input);
        }

        public static void Send(IWebDriver driver, string xpath, string input)
        {
            driver.FindElement(By.XPath(xpath)).SendKeys(input);
            Thread.Sleep(500);
        }

        public static bool IsLoading(IWebDriver driver)
        {
            try
            {
                List<string> positiveList = new List<string>(new string[]
                {
                    "spinner-border ml-3 md-spinner",
                    "spinner-border ml-3",
                    "navigation__loading navigation__loading--active"
                });

                try
                {
                    if (driver.PageSource == "")
                    {
                        Console.WriteLine("No page source");
                        return true;
                    }

                    foreach (var identifier in positiveList)
                    {
                        try
                        {
                            if (driver.PageSource.Contains(identifier))
                            {
                                Console.WriteLine($"Loading... Found identifier: {identifier}");
                                Thread.Sleep(500);
                                return true;
                            }
                        }
                        catch
                        {
                            Console.WriteLine($"IsLoading, in foreach - NullRefError");
                        }
                    }
                }
                //catch (System.NullReferenceException e)
                //{
                //    Console.WriteLine($"IsLoading - NullRefError: {e}");
                //}
                //catch (Exception e)
                //{
                //    Console.WriteLine($"IsLoading - Something else went wrong: {e}");
                //}
                catch
                {
                    // Log this?
                }

                return false;
            }
            //catch (OpenQA.Selenium.NoSuchWindowException)
            //{
            //    Console.WriteLine("No such window!");
            //    return false;
            //}
            //catch
            //{
            //    Console.WriteLine("Another error");
            //    return false;
            //}
            catch
            {
                // Log this?
                return false;
            }
        }
    }
}