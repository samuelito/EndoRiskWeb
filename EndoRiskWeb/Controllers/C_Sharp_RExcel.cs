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
using System.Threading;

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
                //Excel.Range excelRange = null;
                Excel.Range excelLine = null;
               
                //Start Excel Application and open the workbook.
                excelApp = new Excel.Application();
                excelApp.Visible = false;
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
                excelWorksheet.Columns.ClearFormats();
                excelWorksheet.Rows.ClearFormats();
                excelLine = excelWorksheet.Cells;
                excelLine = excelWorksheet.Rows[excelWorksheet.UsedRange.Rows.Count];
                excelLine.Activate();
              
                int rowOffset = 0;
                for (int i = 0; i <= excelWorksheet.Columns.Count- 1; i++)
                    {
                        if (excelWorksheet.Cells[1, i + 1].Value() == null )
                        {
                            break;
                        }

                        else if(excelWorksheet.Cells[1, i + 1].Value() == "Y")
                        {
                            excelWorksheet.Cells[(excelWorksheet.UsedRange.Rows.Count + 1) - rowOffset, i + 1] = 0;
                          
                            break;
                        }

                        for (int j = 0; j <= answerList.GetLength(0);j++ )
                        {                           
                            if (excelWorksheet.Cells[1, i + 1].Value().ToString() == answerList[j, 1].ToString())
                            {
                                
                                excelWorksheet.Cells[(excelWorksheet.UsedRange.Rows.Count + 1) - rowOffset, i + 1] = answerList[j, 0];
                             
                                if (rowOffset == 0) { rowOffset++; }
                                
                                break;
                            }
                        }
                    }

                //Open Severity Excel Workbook
                excelWorkbook = (Excel._Workbook)excelApp.Workbooks.Open("C:\\Users\\eddie.perez\\Desktop\\Severity_Data.csv", Missing.Value, ReadOnly: false);
                
                excelWorkbook = excelApp.ActiveWorkbook;
                excelWorksheet = excelApp.ActiveSheet as Excel._Worksheet;
                excelWorksheet = (Excel.Worksheet)excelWorkbook.Worksheets.get_Item(1);
                excelWorksheet.Activate();
                excelWorksheet.Columns.ClearFormats();
                excelWorksheet.Rows.ClearFormats();
                excelLine = excelWorksheet.Cells;
                excelLine = excelWorksheet.Rows[excelWorksheet.UsedRange.Rows.Count];
                excelLine.Activate();

                rowOffset = 0;
                for (int i = 0; i <= excelWorksheet.Columns.Count - 1; i++)
                {
                    if (excelWorksheet.Cells[1, i + 1].Value() == null)
                    {
                        break;
                    }

                    else if (excelWorksheet.Cells[1, i + 1].Value() == "Severity")
                    {
                        excelWorksheet.Cells[(excelWorksheet.UsedRange.Rows.Count + 1) - rowOffset, i + 1] = "I-II";
                     
                        break;
                    }

                    for (int j = 0; j <= answerList.GetLength(0); j++)
                    {
                        if (excelWorksheet.Cells[1, i + 1].Value().ToString() == answerList[j, 1].ToString())
                        {
                            excelWorksheet.Cells[(excelWorksheet.UsedRange.Rows.Count + 1) - rowOffset, i + 1] = answerList[j, 0];
                            
                            if (rowOffset == 0) { rowOffset++; }
                            break;
                        }
                    }
                }            

                //Run the macro in Excel that run scripts in R workbench
                RunMacro(excelApp, new Object[] { "Prediction.xlsm!Prediction.Prediction" });        
                 
                //Get the prediction value from excel sheet     
                excelWorkbook = excelApp.Workbooks[1];
                excelWorkbook = excelApp.ActiveWorkbook;
                excelWorksheet = (Excel.Worksheet)excelWorkbook.Worksheets.get_Item(1);
                excelWorksheet.Activate();

                object lifetime_risk_value = excelWorksheet.Cells[2, 1].Value;
                object severity = excelWorksheet.Cells[2, 2].Value;
                object [] test_results = new object[2];
                test_results[0] = (double)lifetime_risk_value;
                test_results[1] = (string)severity;

                //Save and close current Excel Workbook
                excelWorkbook.Close(true);

                excelApp.Quit();
               // Marshal.FinalReleaseComObject(excelApp);
                //Quit the Excel Application and releaese all Excel application objects used in the program from memory
                if (excelApp != null)
                {
                    KillExcelFileProcess("Prediction", "R Console 32-bit");                    
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
                try
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                    obj = null;
                }

                catch (Exception)
                {
                    obj = null;
                }

                finally
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }

           
                
            //    xlApp.DisplayAlerts = false;
            //    xlApp.AskToUpdateLinks = false;
         

            //Method to run Excel macro
            private void RunMacro(object oApp, object[] oRunArgs)
            {
                   oApp.GetType().InvokeMember("Run", System.Reflection.BindingFlags.Default | System.Reflection.BindingFlags.InvokeMethod,
                   null, oApp, oRunArgs);
            }

            //Method to end the process executing Excel
            private void KillExcelFileProcess(string excelFileName, string R_Program)
            {
                
                var excelProcess = from proc in System.Diagnostics.Process.GetProcessesByName("EXCEL") select proc;

                foreach (var process in excelProcess)
                {
                    if (process.MainWindowTitle == "Excel")
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