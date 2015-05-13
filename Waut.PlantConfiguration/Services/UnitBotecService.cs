using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Waut.PlantConfiguration.Models;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Collections.ObjectModel;

namespace Waut.PlantConfiguration.Services
{
    public class UnitBotecService : ObservableCollection<Unit> 
    {
        public Unit myUnit = new Unit();

        public UnitBotecService()
        {

        }
        #region GetUnit
        public List<Unit> GetUnits(string tableName)
        {
            List<Unit> list = new List<Unit>();

            //***************Sample Entry***************
            string[] arr = { "SAMPLE", "121", "Type", "Unit", "Format", "Krones", "Project", "1", "2", "3", "4", "PLCWhat", "SheetWhat" };
            myUnit = new Unit();
            myUnit.Description = arr[0]; myUnit.ID = 1234; myUnit.Type = arr[2]; myUnit.KronesSectorSymbol = arr[3]; myUnit.ProjectSectorSymbol = arr[4]; myUnit.KronesSymbol = arr[5];
            myUnit.ProjectSymbol = arr[6]; myUnit.PLCID = arr[7]; myUnit.Description = arr[8]; myUnit.ClassID = arr[9];
            list.Add(myUnit);
            //***************Sample Entry***************
#if         USINGPROJECTSYSTEM  // Set Access connection and select strings.
            string con = @"Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties='Excel 8.0;HDR=Yes;'";
#else
            //string con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + tableName;
            //string con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=SQL2005\SK_BOTEC_DB;Initial Catalog=KR_ONITSHA_F1_config;Persist Security Info=True;User ID=sa;Password=delphi;";
            string con = @"Provider=SQLOLEDB;Data Source=SQL2005\SK_BOTEC_DB;Initial Catalog=KR_ONITSHA_F1_config;Persist Security Info=True;User ID=sa;Password=delphi;";

#endif
            string num = "123";//Temp number for control module - Please assign actual value at later stage

            using (OleDbConnection connection = new OleDbConnection(con))
            {
                connection.Open();
                
                //***************Get all worksheet names***************
                DataTable dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
               

                        OleDbCommand command = new OleDbCommand("SELECT * FROM [" + tableName + "]", connection);
                        //command.Parameters.AddWithValue("@mySheet", excelSheets[k]);
                       
                        using (OleDbDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    var column = dr[j];
                                    arr[j] = column.ToString();
                                }
                                //load integer element from 'arr' into x
                                int x = 0;
                                Int32.TryParse(num, out x);

                                if (arr[6] != "" && arr[6] != "DESCRIPTION")
                                {
                                    myUnit = new Unit();

                                    myUnit.ID = x;
                                    myUnit.Name = arr[2];
                                    myUnit.ShortName = arr[8];
                                    myUnit.KronesSectorSymbol = arr[1];
                                    myUnit.ProjectSectorSymbol = arr[4];
                                    myUnit.ProjectSymbol = arr[2];
                                    myUnit.KronesSymbol = arr[3];
                                    myUnit.PLCID = arr[2];
                                    myUnit.Description = arr[3];
                                    myUnit.ClassID = arr[4];
                                    Console.WriteLine(myUnit.Name);
                                    //myUnit.ControlModules.........

                                    list.Add(myUnit);
                                }
                            }
                        }
                        list.Add(myUnit);    
                }
                //*****SUMMARY*****
                Console.WriteLine("****LOAD SUMMARY*****");
                Console.WriteLine("Load Successfull.");
                Console.WriteLine("****LOAD SUMMARY*****");
                //*****SUMMARY*****
            
            return list;
        }
        #endregion GetUnit
    }
}
