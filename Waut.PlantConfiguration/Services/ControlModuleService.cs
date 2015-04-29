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



            ControlModule cm = new ControlModule();
            cm.Description = "af as asd as ";
            ExcelRead();





            list.Add(cm);


            return list;
        }

        public static void/*string[]*/ ExcelRead()
        {
            string[] arr = { "wort inlet holding vessel", "121", "Type", "Unit", "Format", "Krones", "Project", "1", "2", "3", "4" };

            string con = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\snel\Source\Repos\NewRepo\Waut.PlantConfiguration\Data\SAB_ONITSHSA_IO_List_BH_PLC1_REV14.xls;Extended Properties='Excel 8.0;HDR=Yes;'";
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
            // return arr;
        }
    }
}
