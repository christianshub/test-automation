using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.IO;
using System.Threading;


namespace TestAutomation
{
    internal class DoSteps
    {
        private IWebDriver Driver;
        private string Path;
        private int waittime = 10;

        public DoSteps(IWebDriver driver, string path)
        {
            Driver = driver;
            Path = path;
        }

        public static void Log(string logMessage, TextWriter w)
        {
            //w.Write("\r\nLog Entry : ");
            //w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            //w.WriteLine("  :");
            w.WriteLine($"{logMessage}");
            //w.WriteLine("-------------------------------");
        }

        public static void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }

        public void StartSteps()
        {               
           
            var logName = "log_" + System.IO.Path.GetFileName(Path).Split('.')[0] + ".txt";

            if(File.Exists(logName))
            {
                File.Delete(logName);
            }            

            using (StreamWriter w = File.AppendText(logName))
            {
                           
                Parser csv = new Parser(Path);

                var data = csv.GetData();

                int count = 0;
                while (count < data.Count)
                {
                    if (count < 0)
                    {
                        break;
                    }

                    Helper.DetectToasters(Driver);

                    var row = data[count];
                    if (Helper.IsLoading(Driver))
                    {
                        continue;
                    }

                    if ((row.XPath != "") && (row.Input != "") && (row.PerformAction != "") &&
                        (row.StepText != "") &&
                        (row.WindowTitle != ""))
                    {
                        try
                        {
                            Console.WriteLine($"Count: {count}, Step: {row.StepText}, Input: {row.Input}, Action: {row.PerformAction}, WindowTitle: {row.WindowTitle}");
                            Log($"Step {count}: {row.StepText}", w);

                            bool found = false;
                            foreach (string handle in Driver.WindowHandles)
                            {
                                IWebDriver popup = Driver.SwitchTo().Window(handle);

                                if ((row.WindowTitle.Contains(popup.Title)) || (popup.Title.Contains(row.WindowTitle)))
                                {
                                    found = true;
                                    break;
                                }
                            }

                            if (!found)
                            {
                                Console.WriteLine("  -- WARNING: Window not found, retrying");
                                Log("  -- WARNING: Window not found, retrying", w);
                                Thread.Sleep(500);
                                continue;
                            }

                            if (row.PerformAction.Contains("Open"))
                            {
                                try
                                {
                                    Driver.Navigate().GoToUrl(row.Input);
                                }
                                catch (Exception e)
                                {
                                    Log("  -- ERROR: At Open, retrying the step prior", w);
                                    Console.WriteLine($"  -- ERROR: At Open, retrying the step prior");
                                    count--;
                                    continue;
                                }
                            }
                            if (row.PerformAction.Contains("Click"))
                            {
                                try
                                {
                                    Helper.WaitClick(Driver, waittime, row.XPath);
                                }
                                catch (Exception e)
                                {
                                    Log("  -- ERROR: At WaitClick, retrying the step prior", w);
                                    Console.WriteLine($"  -- ERROR: At WaitClick, retrying the step prior");
                                    count--;
                                    continue;
                                }
                            }

                            if (row.PerformAction.Contains("Send"))
                            {
                                try
                                {
                                    Helper.WaitSend(Driver, waittime, row.XPath, row.Input);
                                }
                                catch
                                {
                                    Log("  -- ERROR: At WaitSend, retrying the step prior", w);
                                    Console.WriteLine($"  -- ERROR: At WaitSend, retrying the step prior");
                                    count--;
                                    continue;
                                }
                            }

                            if (row.PerformAction.Contains("Enter"))
                            {
                                try
                                {
                                   Actions builder = new Actions(Driver);        
                                   builder.SendKeys(Keys.Enter);
                                }
                                catch
                                {
                                    Log("  -- ERROR: At pressing Enter", w);
                                    Console.WriteLine($"  -- ERROR: At pressing Enter");
                                    count--;
                                    continue;
                                }
                            }

                            count++;
                            continue;
                        }
                        catch (OpenQA.Selenium.WebDriverTimeoutException e)
                        {
                            Log($"  -- ERROR: Webdriver timeout, resetting step", w);
                            Console.WriteLine($"Webdriver timeout, resetting step: {e}");
                            continue;
                        }
                    }
                    count++;
                }
            }
        }
    }
}