using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;

namespace TestAutomation
{
    internal class Parser
    {
        public struct Data
        {
            public Data(string xpath, string performaction, string input, string steptext, string windowtitle)
            {
                XPath = xpath;
                PerformAction = performaction;
                Input = input;
                StepText = steptext;
                WindowTitle = windowtitle;
            }

            public string XPath { get; set; }
            public string PerformAction { get; set; }
            public string Input { get; set; }
            public string StepText { get; set; }
            public string WindowTitle { get; set; }

            public override string ToString() => $"(XPath: {XPath}, PerformAction: {PerformAction}, Input: {Input}, Steptext: {StepText}, WindowTitle: {WindowTitle})";
        }

        private string Path;

        public Parser(string path)
        {
            Path = path;
        }

        public static List<string> GetExcelFiles()
        {
            string path = Directory.GetCurrentDirectory();
            string[] files = Directory.GetFiles(path);

            List<string> excelFiles = new List<string>();

            foreach (var file in files)
            {
                try
                {
                    if (file.Contains(".xlsx"))
                    {
                        excelFiles.Add(file);
                    }
                }
                catch
                {
                    // Log this?
                }
            }

            return excelFiles;
        }

        public List<Data> GetData()
        {
            var xlApp = new Application();
            var xlWorkbook = xlApp.Workbooks.Open(Path);
            var xlWorksheet = xlWorkbook.Sheets[1];
            var xlRange = xlWorksheet.UsedRange;

            List<Data> list = new List<Data>();

            int count = 0;
            int rowCount = 20;
            int colCount = 5;
            for (int i = 2; i <= rowCount; i++)
            {
                Data data = new Data("", "", "", "", "");

                for (int j = 1; j <= colCount; j++)
                {
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {
                        if (count == 0)
                        {
                            data.XPath = xlRange.Cells[i, j].Value2.ToString();
                        }

                        if (count == 1)
                        {
                            data.PerformAction = xlRange.Cells[i, j].Value2.ToString();
                        }

                        if (count == 2)
                        {
                            data.Input = xlRange.Cells[i, j].Value2.ToString();
                        }

                        if (count == 3)
                        {
                            data.StepText = xlRange.Cells[i, j].Value2.ToString();
                        }

                        if (count == 4)
                        {
                            data.WindowTitle = xlRange.Cells[i, j].Value2.ToString();
                        }
                        count++;
                    }
                }

                list.Add(data);
                count = 0;
            }

            xlWorkbook.Close();
            return list;
        }
    }
}