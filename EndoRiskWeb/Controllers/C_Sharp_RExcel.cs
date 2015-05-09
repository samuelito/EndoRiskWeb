//Course: ICOM 5047
//Advisors: Fernando Vega, Leo Solorzano
//Author: Eddie Pérez Martell
//Topic: Visual Studio C# connecton with R Language

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using VBIDE = Microsoft.Vbe.Interop;
using Microsoft.Office.Core;
using Microsoft.VisualBasic;
using Microsoft.Internal;
using EndoRiskWeb.Models;
using System.IO;
using System.Runtime.InteropServices;

namespace EndoRiskWeb.Controllers
{
    public class C_Sharp_RExcel
    {
        public object[] Prediction(object[,] answerList)
        {
            //Make sure excel version is an US version
            System.Globalization.CultureInfo oldCI;
            oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            //Define your Excel Application Objects
            Excel.Application excelApp = null;
            Excel._Workbook excelWorkbook = null;
            Excel._Worksheet excelWorksheet = null;
            Excel.Range excelLine = null;

            //Start Excel Application and open the workbook.
            excelApp = new Excel.Application();
            excelApp.Visible = true;
            excelApp.DisplayAlerts = false;

            //Open Prediction Excel Workbook
            excelWorkbook = (Excel._Workbook)excelApp.Workbooks.Open("C:\\Users\\eddie.perez\\Desktop\\Prediction.xlsm", Missing.Value, ReadOnly: false);


            //Enable RExcel Add-In
            Excel.AddIn ad = (Excel.AddIn)excelApp.AddIns.get_Item(1);
            ad.Installed = true;

            excelWorkbook = excelApp.ActiveWorkbook;
            excelWorksheet = excelApp.ActiveSheet as Excel._Worksheet;

            //Fill each cell with user answer
            excelWorksheet = (Excel.Worksheet)excelWorkbook.Worksheets.get_Item(2);
            excelWorksheet.Activate();
            excelLine = excelWorksheet.Cells;
            excelLine.Activate();

            for (int i = 0; i <= excelWorksheet.Columns.Count - 1; i++)
            {
                if (excelWorksheet.Cells[1, i + 1].Value() == null)
                {
                    break;
                }

                else if (excelWorksheet.Cells[1, i + 1].Value() == "Y")
                {
                    excelWorksheet.Cells[2, i + 1] = 0;

                    break;
                }

                for (int j = 0; j <= answerList.GetLength(0); j++)
                {
                    if (excelWorksheet.Cells[1, i + 1].Value().ToString() == answerList[j, 1].ToString())
                    {

                        excelWorksheet.Cells[2, i + 1] = answerList[j, 0];

                        break;
                    }
                }
            }

            excelWorksheet = (Excel.Worksheet)excelWorkbook.Worksheets.get_Item(3);
            excelWorksheet.Activate();
            excelLine = excelWorksheet.Cells;
            excelLine.Activate();

            for (int i = 0; i <= excelWorksheet.Columns.Count - 1; i++)
            {
                if (excelWorksheet.Cells[1, i + 1].Value() == null)
                {
                    break;
                }

                else if (excelWorksheet.Cells[1, i + 1].Value() == "Severity")
                {
                    excelWorksheet.Cells[2, i + 1] = "I-II";

                    break;
                }

                for (int j = 0; j <= answerList.GetLength(0); j++)
                {
                    if (excelWorksheet.Cells[1, i + 1].Value().ToString() == answerList[j, 1].ToString())
                    {
                        excelWorksheet.Cells[2, i + 1] = answerList[j, 0];

                        break;
                    }
                }
            }

            //Run the macro in Excel that run scripts in R workbench
            excelApp.Run("Prediction.xlsm!Prediction.Prediction");

            excelWorksheet = (Excel.Worksheet)excelWorkbook.Worksheets.get_Item(1);
            excelWorksheet.Activate();

            object lifetime_risk_value = excelWorksheet.Cells[2, 1].Value;
            object severity = excelWorksheet.Cells[2, 2].Value;
            object[] test_results = new object[2];
            test_results[0] = (double)lifetime_risk_value;
            test_results[1] = (string)severity;

            excelApp.Run("CloseExcel");

            //Quit the Excel Application and releaese all Excel application objects used in the program from memory
            if (excelApp != null)
            {
                KillExcelFileProcess();
                releaseObject(excelWorksheet);
                releaseObject(excelWorkbook);
                releaseObject(excelApp);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            return test_results;
        }

        //Method to relase all Excel application objects used
        private void releaseObject(object obj)
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            obj = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        //Method to end the process executing Excel
        private void KillExcelFileProcess()
        {
            var excel_process = from proc in System.Diagnostics.Process.GetProcessesByName("EXCEL") select proc;

            foreach (var process in excel_process)
            {
                process.Kill();
            }
            var R_process = from proc in System.Diagnostics.Process.GetProcessesByName("Rgui") select proc;

            foreach (var process in R_process)
            {
                if (process.MainWindowTitle == "R Console (32-bit)")
                    process.Kill();
            }
        }
    }
}