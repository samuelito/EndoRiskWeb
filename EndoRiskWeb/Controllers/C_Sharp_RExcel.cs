﻿//Course: ICOM 5047
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
//using STATCONNECTORCLNTLib;
//using StatConnectorCommonLib;
//using STATCONNECTORSRVLib;
using EndoRiskWeb.Models;
using System.IO;

namespace EndoRiskWeb.Controllers
{
    public class C_Sharp_RExcel
    {
             public double[] Macro(object[,] answerList)
             {
                //Make sure excel version is an US version
                System.Globalization.CultureInfo oldCI;
                oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

                //Define your Excel Application Objects
                Excel.Application excelApp = null;
                Excel._Workbook excelWorkbook = null;
                Excel._Worksheet excelWorksheet = null;
                Excel.Range excelRange = null;
                Excel.Range excelLine = null;
                //Excel.Range excelColumn = null;
                Excel.Range lastRow = null;
                VBIDE.VBComponent VBAmodule = null;
               
                //Start Excel Application and open the workbook.
                excelApp = new Excel.Application();
                excelApp.Visible = true;
                excelApp.DisplayAlerts = false;
                                
                //Open Prediction Excel Workbook
                excelWorkbook = (Excel._Workbook)excelApp.Workbooks.Open("C:\\Users\\owner\\Desktop\\VS_2013\\Proyecto_EndoRisk\\Prediction.xlsm", Missing.Value, ReadOnly: false);
                
                //Enable RExcel Add-In
                Excel.AddIn ad = (Excel.AddIn)excelApp.AddIns.get_Item(1);
                ad.Installed = true;

                excelWorkbook = excelApp.ActiveWorkbook;
                excelWorksheet = excelApp.ActiveSheet as Excel._Worksheet;
                ///excelRange = excelWorksheet.Cells;

                //string newSymptom = "VOM";
                // //Check if a new symptom column for the dataframe will be added
                //if (newSymptom != null)
                //{
                //    //excelLine = excelWorksheet.Cells;
                //    excelLine = excelWorksheet.Rows[1];
                //    excelLine.Activate();
                //   // 
                //    for (int i = 1; i <= excelLine.Count; i++)
                //    {
                //        if (excelLine[1,i].ToString() == "Y")
                //        {
                //            excelColumn = excelWorksheet.Cells;
                //            excelColumn = excelWorksheet.Columns[i];
                //            excelColumn.Activate();
                //            excelLine.Insert();
                //            excelLine[1, i - 1] = newSymptom;
                //            break;
                //        }
                //    }
                // }

                //Fill each cell with user answer
                excelWorksheet = (Excel.Worksheet)excelWorkbook.Worksheets.get_Item(2);
                excelWorksheet.Activate();            
                lastRow = excelWorksheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
                //excelRange = excelWorksheet.get_Range("A1", lastRow);
                //excelRange.Activate();
                //int lastUsedRow = lastRow.Row;
                //iTotalColumns = xlWorkSheet.UsedRange.Columns.Count;
                //iTotalRows = xlWorkSheet.UsedRange.Rows.Count;

                ////These two lines do the magic.
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
                            excelWorksheet.Cells[excelWorksheet.UsedRange.Rows.Count + 1, i + 1] = 0;
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
                excelWorkbook = (Excel._Workbook)excelApp.Workbooks.Open("C:\\Users\\owner\\Desktop\\VS_2013\\Proyecto_EndoRisk\\Severity_Data.csv", Missing.Value, ReadOnly: false);
                excelWorkbook = excelApp.ActiveWorkbook;
                excelWorksheet = excelApp.ActiveSheet as Excel._Worksheet;
                excelRange = excelWorksheet.Cells;

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
                        excelWorksheet.Cells[excelWorksheet.UsedRange.Rows.Count + 1, i + 1] = "I-II";
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
                //RunMacro(excelApp, new Object[] { "Prediction.xlsm!Prediction.Prediction" });
                 
                //Get the prediction value from excel sheet     
                excelWorkbook = excelApp.Workbooks[1];
                excelWorkbook = excelApp.ActiveWorkbook;
                excelWorksheet = (Excel.Worksheet)excelWorkbook.Worksheets.get_Item(1);
                excelWorksheet.Activate();

                object lifetime_risk_value = excelWorksheet.Cells[2, 1].Value;// *100;
                object severity_value = excelWorksheet.Cells[2, 2].Value;
                double [] test_results = new double[2];
                test_results[0] = (double)lifetime_risk_value;
                test_results[1] = (double)severity_value;

                //Save and close current Excel Workbook
                excelWorkbook.Close(true);

                //Quit the Excel Application and releaese all Excel application objects used in the program from memory
                if (excelApp != null)
                {
                    KillExcelFileProcess("Prediction", "R Console 32-bit");
                    releaseObject(excelRange);
                    releaseObject(excelWorksheet);
                    releaseObject(VBAmodule);
                    releaseObject(excelWorkbook);
                    releaseObject(excelApp);
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

            //private static Excel._Workbook CreateWorkbook(Excel.Application excelApp, Excel._Workbook excelWorkbook,
            //                                              Excel._Worksheet excelWorksheet, Excel.Range excelRange,
            //                                              Excel.Range excelLine, VBIDE.VBComponent VBAmodule, object[,] answerList)
            //{
            //    excelWorkbook = (Excel._Workbook)(excelApp.Workbooks.Add(Missing.Value));
            //    excelWorkbook = excelApp.ActiveWorkbook;
            //    excelWorksheet = excelApp.Worksheets.Add(Missing.Value,1,2) as Excel._Worksheet;
            //    excelWorksheet = excelApp.ActiveSheet as Excel._Worksheet;
            //    excelWorksheet = excelApp.ActiveSheet as Excel._Worksheet;
            //    excelRange = excelWorksheet.Cells;

            //    excelWorkbook.SaveAs("C:\\Users\\owner\\Desktop\\Prediction.xlsm", Excel.XlFileFormat.xlOpenXMLWorkbookMacroEnabled, Missing.Value,
            //    Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange,
            //    Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                
            //    Excel.AddIn ad = (Excel.AddIn)excelApp.AddIns.get_Item(1);
            //    ad.Installed = true;

            //    excelWorksheet = (Excel.Worksheet)excelWorkbook.Worksheets.get_Item(2);
            //    excelWorksheet.Activate();
            //    //excelLine = excelWorksheet.Cells;
            //    //excelLine = excelWorksheet.Rows[2];
            //    //excelLine.Insert();
            //    excelRange = excelWorksheet.Cells;

            //    VBAmodule = excelWorkbook.VBProject.VBComponents.Add(VBIDE.vbext_ComponentType.vbext_ct_StdModule);
            //    VBAmodule.CodeModule.AddFromString(WriteMacro());

            //    return excelWorkbook;
            //}

            //private static string WriteMacro()
            //{

            //    System.Text.StringBuilder macroCode = new System.Text.StringBuilder();

            //    macroCode.Append("Sub Prediction()" + "\n");
            //    macroCode.Append("  RInterface.StartRServer" + "\n");
            //    macroCode.Append("  Sheets('Datos_Modelo_Regresion').Select" + "\n"); 
            //    macroCode.Append("  Range('A3').Select" + "\n");
            //    macroCode.Append("  Selection.Copy" + "\n");
            //    macroCode.Append("  Range('A2').Select" + "\n");
            //    macroCode.Append("  Range(Selection, Selection.End(xlToRight)).Select" + "\n");
            //    macroCode.Append("  Selection.PasteSpecial Paste:=xlPasteFormats, Operation:=xlNone, SkipBlanks:=False, Transpose:=False" + "\n");
            //    macroCode.Append("  ActiveWorkbook.Save" + "\n");
            //    macroCode.Append("  Sheets('Codigo_Prediccion_R').Select" + "\n");
            //    macroCode.Append("  RInterface.RRun ('x<-source('C:/Users/owner/Desktop/VS_2013/Proyecto_EndoRisk/R_Scripts/script_to_read_profe.R')')" + "\n");
            //    macroCode.Append("  Range('A2') = RInterface.GetRExpressionValueToVBA('x')" + "\n");
            //    macroCode.Append("  Range('A2').Select" + "\n");
            //    macroCode.Append("  Selection.NumberFormat = '0.00%'" + "\n");
            //    macroCode.Append("  Sheets('Datos_Modelo_Regresion').Select" + "\n");
            //    macroCode.Append("  Rows('2:2').Select" + "\n");
            //    macroCode.Append("  Selection.Delete Shift:=xlUp" + "\n");
            //    macroCode.Append("  Sheets('Codigo_Prediccion_R').Select" + "\n");
            //    macroCode.Append("   RInterface.StopRServer" + "\n");
            //    macroCode.Append("End Sub");
            
            //    return macroCode.ToString();
            //}

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