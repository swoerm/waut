using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waut.PlantConfiguration.Models;
//Excel
using System.Xml.Serialization;
using System.IO;
using System.Data;
using System.Diagnostics;
using System.Data.OleDb;
//Excel


namespace Waut.PlantConfiguration.Services
{
    public class ControlModuleService : IControlModuleService
    {
        public string FileName { get; set; }

        public ControlModuleService()
        {

        }

        public List<ControlModule> GetControlModules(string FileName)
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            List<ControlModule> list = new List<ControlModule>();
            ControlModule cm = new ControlModule();

            //***************Sample Entry***************
            string[] arr = { "SAMPLE", "121", "Type", "Unit", "Format", "Krones", "Project", "1", "2", "3", "4" };
            cm = new ControlModule();
            cm.Description = arr[0];cm.Number = 1234;cm.Type = arr[2];cm.UoM = arr[3];cm.Format = arr[4];cm.KronesSymbol = arr[5];
            cm.ProjectSymbol = arr[6];cm.Symbol1 = arr[7];cm.Symbol2 = arr[8];cm.Symbol3 = arr[9];cm.Symbol4 = arr[10];
            list.Add(cm);
            //***************Sample Entry***************

            string con = @"Provider=Microsoft.JET.OLEDB.4.0;Data Source=C:\Users\snel\Desktop\PLC1.xls;Extended Properties='Excel 8.0;HDR=Yes;'";
            //string con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\..\..\Waut.PlantConfiguration\Data\SAB_ONITSHSA_IO_List_BH_PLC1_REV14.xls;Extended Properties='Excel 8.0;HDR=Yes;'";
            string num = "123";
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

                for (int k = 0; k < excelSheets.Length; k++)//Loop through all worksheets  excelSheets.Length
                {
                    OleDbCommand command = new OleDbCommand("select * from [" + excelSheets[k] + "]", connection);//[BH1PB01$]

                    try  //Exception to catch missing worksheet
                    {
                        using (OleDbDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    var column = dr[j];
                                    arr[j] = column.ToString();
                                }
                                int x = 0;//load integer element from 'arr' into x
                                Int32.TryParse(num, out x);

                                if (arr[6] != "" && arr[6] != "DESCRIPTION")
                                {
                                    cm = new ControlModule();

                                    cm.Description = arr[0];
                                    cm.Number = x;
                                    cm.Type = arr[2];
                                    cm.UoM = arr[3];
                                    cm.Format = arr[4];
                                    cm.Symbol1 = arr[5];
                                    cm.Symbol2 = arr[6];
                                    cm.Symbol3 = arr[7];
                                    cm.Symbol4 = arr[8];
                                    cm.Sheet = excelSheets[k];
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
            list.Add(cm);
            return list;
        }
    }
}
