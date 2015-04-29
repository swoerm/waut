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
            List<ControlModule> list = new List<ControlModule>();


            ExcelRead();
            ControlModule cm = new ControlModule();
            cm.Description = ExcelRead();
            





            list.Add(cm);


            return list;
        }
        
        public static string ExcelRead()
        {
            Console.WriteLine("hefsksljdz");
            string woord = "hello";
            //string[] arr = { "wort inlet holding vessel", "121", "Type", "Unit", "Format", "Krones", "Project", "1", "2", "3", "4" };
            //var x;
            string con = @"Provider=Microsoft.JET.OLEDB.4.0;Data Source=C:\Users\snel\Desktop\PLC1.xls;Extended Properties='Excel 8.0;HDR=Yes;'";
            using (OleDbConnection connection = new OleDbConnection(con))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("select * from [BH1PB01$]", connection);
                using (OleDbDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var row1Col0 = dr[0];
                        var row1Col1 = dr[1];
                        Console.WriteLine(row1Col0);
                        Console.WriteLine(row1Col1);
                    }
                }
            }
            return woord;
            // return arr;
        }
    }
}
