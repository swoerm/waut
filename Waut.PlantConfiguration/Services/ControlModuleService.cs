using System;
using System.Collections.Generic;
using Waut.PlantConfiguration.Models;
//Excel
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Collections.ObjectModel;


//Excel  

namespace Waut.PlantConfiguration.Services
{
    public class ControlModuleService : ObservableCollection<ControlModule> 
    {
        public string FileName { get; set; }
        

        public ControlModuleService()
        {

        }

        #region GetControlModule
        public List<ControlModule> GetControlModules(string FileName)
        {

            List<ControlModule> list = new List<ControlModule>();/////

            string[] arr = new string[40];

            //*****************************************************************************************************************
            //*****************************************************************************************************************
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

            ExcelFile ef = ExcelFile.Load(FileName);

            // Select the first worksheet from the file.
            ExcelWorksheet ws = ef.Worksheets[0];

            DataTable dataTable = ws.CreateDataTable(new CreateDataTableOptions()
            {
                ColumnHeaders = true,
                StartRow = 1,
                NumberOfColumns = 5,
                NumberOfRows = ws.Rows.Count - 1,
                Resolution = ColumnTypeResolution.AutoPreferStringCurrentCulture
            });

            // Write DataTable content
            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", row[0], row[1], row[2], row[3], row[4]);
                Console.WriteLine();
            }

            //Console.WriteLine(sb.ToString());
            //*****************************************************************************************************************
            //*****************************************************************************************************************
      

            //***************Sample Entry***************
            int wrongRead = 0;
            string con = @";Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties='Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0'";
            using (OleDbConnection connection = new OleDbConnection(con))
            {
                connection.Open();
                
                //***************Get all worksheet names***************
                DataTable dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
               
                if (dt == null)
                {
                    return null;
                }

                String[] excelSheets = new String[dt.Rows.Count];
                int i = 0;


                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[i] = row["TABLE_NAME"].ToString();
                    i++;
                }

                for (int j = 0; j < excelSheets.Length; j++)
                {
                    Console.WriteLine(excelSheets[j]);
                }
                Console.WriteLine(excelSheets.Length);

                //***************Get all worksheet names***************
                for (int k = 0; k < excelSheets.Length; k++)//Loop through all worksheets
                {
                    try  //Exception to catch missing worksheet
                    {
                        OleDbCommand command = new OleDbCommand("select * from [" + excelSheets[k] + "]", connection);
                        //command.Parameters.AddWithValue("@mySheet", excelSheets[k]);
                       
                        using (OleDbDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                for (int j = 0; j < 23; j++)
                                {
                                    var column = dr[j];
                                    arr[j] = column.ToString();
                                }

                                ControlModule cm = ControlModuleFactory.CreateControlModuleXLS(arr);
                                
                                if (cm != null)
                                {
                                    Console.WriteLine(cm.Description);

                                    if (string.IsNullOrEmpty(cm.Symbol1))
                                    {
                                        Console.WriteLine("sf");
                                        wrongRead++;
                                    }
                                    list.Add(cm); 
                                }
                                    
                            }
                        }
                           
                    }
                    catch (OleDbException ex)
                    {
                        Console.WriteLine(ex.ToString());//Display 
                    }
                }
            }
            Console.WriteLine("Wrong: " + wrongRead);
            return list;
        #endregion GetControlModule
        }
    }
}



