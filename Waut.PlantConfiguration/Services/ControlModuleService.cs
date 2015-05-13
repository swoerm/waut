using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waut.PlantConfiguration.Models;
//using Microsoft.Practices.Prism.Mvvm;
//Excel
using System.Xml.Serialization;
using System.IO;
using System.Data;
using System.Diagnostics;
using System.Data.OleDb;
using System.Collections.ObjectModel;
using System.Collections.Specialized;


namespace Waut.PlantConfiguration.Services
{
    public class ControlModuleService : ObservableCollection<ControlModule> //, IControlModuleService
    {
        public string FileName { get; set; }

        public ControlModuleService()
        {

        }

        #region GetControlModule
        public List<ControlModule> GetControlModules(string FileName)
        {
            Console.WriteLine("Added to list!!");
            Console.WriteLine(Directory.GetCurrentDirectory());
            List<ControlModule> list = new List<ControlModule>();
            ControlModule cm = new ControlModule();
            Console.WriteLine(FileName);

            //***************Sample Entry***************
            string[] arr = { "SAMPLE", "121", "Type", "Unit", "Format", "Krones", "Project", "1", "2", "3", "4" ,"PLCWhat","SheetWhat"};
            cm = new ControlModule();
//            cm.Description = arr[0];cm.Number = 1234;cm.Type = arr[2];cm.UoM = arr[3];cm.Format = arr[4];cm.KronesSymbol = arr[5];
//            cm.ProjectSymbol = arr[6]; cm.Symbol1 = arr[7]; cm.Symbol2 = arr[8]; cm.Symbol3 = arr[9]; cm.Symbol4 = arr[10]; cm.Sheet = arr[11]; cm.File = arr[12];
            list.Add(cm);
            //***************Sample Entry***************

            string con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties='Excel 8.0;HDR=Yes;'";
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

                for (int k = 0; k < 5/*excelSheets.Length*/; k++)//Loop through all worksheets  excelSheets.Length
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

                                    cm.Description = arr[6];
                                    cm.Number = x;
                                    cm.Type = arr[8];//
                                    cm.UoM = arr[1];
                                    cm.Format = arr[4];//
                                    cm.Symbol1 = arr[2];
                                    cm.Symbol2 = arr[3];
                                    cm.Symbol3 = arr[4];
                                    cm.Symbol4 = arr[5];
                                    //cm.ProjectSymbol = arr[2] + arr[4];//What is the arrangement
                                    //cm.KronesSymbol = arr[3] + arr[5];//What is the arrangement
                                    cm.Sheet = excelSheets[k];
                                    cm.File = FileName;
                                    list.Add(cm);
                                    //Console.WriteLine(cm.Description);
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
            #endregion GetControlModule

            //***************PLAY****************
            string myPlayFile = @"C:\Users\snel\Desktop\BugTypes.mdb";
            
            string myTableName1 = "Categories";
            SaveData(myPlayFile, myTableName1);
            string myTableName2 = "People";
            SaveData(myPlayFile, myTableName2);
          
            string myTempSQL = @"C:\Users\snel\Desktop\BugTypes.mdb";
            string mySQLTable = "NewModule";
            SaveControlModule(list, myTempSQL, mySQLTable);
            //***************PLAY****************
            return list;
        }
        public void SaveControlModule(List<ControlModule> pastList, string fileName, string tableName)
        {
            Console.WriteLine("GotMe");//Just a marker

#if         USINGPROJECTSYSTEM  // Set Access connection and select strings.
            string con = @"Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties='Excel 8.0;HDR=Yes;'";
#else
            string conn = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName;
#endif
            string strAccessSelect = "SELECT * FROM " + tableName;

            // Create the dataset and add the Categories table to it:
           // DataSet myDataSet = new DataSet();
            OleDbConnection myAccessConn = null;
            try
            {
                myAccessConn = new OleDbConnection(conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Failed to create a database connection. \n{0}", ex.Message);
                return;
            }
            try
            {
                OleDbCommand myAccessCommand = new OleDbCommand(strAccessSelect, myAccessConn);
                OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myAccessCommand);

                myAccessConn.Open();

                OleDbCommand addObj = null;//Add a new entry 
                for (int j = 0; j < pastList.Count; j++)
                {
                    Console.WriteLine(pastList[j].UoM);
                    string strSQL = "INSERT INTO NewModule (Description, Type, UoM, Format, Sheet, File) " + "VALUES ( '" + pastList[j].Description + "' , '" +
                    pastList[j].Type + "' ,'" + pastList[j].UoM + "' ,'" + pastList[j].Format + "' ,'" + pastList[j].Sheet + "' ,'" + pastList[j].File + "')";
                    addObj = new OleDbCommand(strSQL, myAccessConn);
                    addObj.ExecuteNonQuery();

                    string strNumSQL = "INSERT INTO NewModule (Number) " + "VALUES ( '" + pastList[j].Number + "')";
                    addObj = new OleDbCommand(strNumSQL, myAccessConn);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Failed to retrieve the required data from the DataBase.\n{0}", ex.Message);
                return;
            }
            finally
            {
                myAccessConn.Close();
            }
        }

        #region PlayArea
        //*****************************PLAY AREA***************************************
        public void SaveData(string fileName, string tableName)
        {
            Console.WriteLine("GotMe");//Just a marker

    #if USINGPROJECTSYSTEM  // Set Access connection and select strings.
            string con = @"Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties='Excel 8.0;HDR=Yes;'";
    #else
            string conn = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName;
    #endif
            string strAccessSelect = "SELECT * FROM "+ tableName;

            // Create the dataset and add the Categories table to it:
            DataSet myDataSet = new DataSet();
            OleDbConnection myAccessConn = null;
            try
            {
                myAccessConn = new OleDbConnection(conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Failed to create a database connection. \n{0}", ex.Message);
                return;
            }

            try
            {
                OleDbCommand myAccessCommand = new OleDbCommand(strAccessSelect, myAccessConn);
                OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myAccessCommand);

                myAccessConn.Open();
                myDataAdapter.Fill(myDataSet, tableName);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Failed to retrieve the required data from the DataBase.\n{0}", ex.Message);
                return;
            }
            finally
            {
                OleDbCommand addObj = null;//Add a new entry sample
                if (tableName == "People")
                {
                    string strSQL = "INSERT INTO People (FirstName , SurName ) " + "VALUES ( 'Beth' , 'Hart' )";
                    addObj = new OleDbCommand(strSQL, myAccessConn);
                    addObj.ExecuteNonQuery();
                }
                myAccessConn.Close();
            }

            DataTableCollection dta = myDataSet.Tables;// A dataset can contain multiple tables, so let's get them all into an array:

            foreach (DataTable dt in dta)
            {
                Console.WriteLine("Found data table {0}", dt.TableName);
            }

            Console.WriteLine("{0} tables in data set", myDataSet.Tables.Count);
            Console.WriteLine("{0} tables in data set", dta.Count);
            Console.WriteLine("{0} rows in Categories table", myDataSet.Tables[tableName].Rows.Count);
            Console.WriteLine("{0} columns in Categories table", myDataSet.Tables[tableName].Columns.Count);
            
            DataColumnCollection drc = myDataSet.Tables[tableName].Columns;
            int i = 0;
            foreach (DataColumn dc in drc)
            {
                Console.WriteLine("Column name[{0}] is {1}, of type {2}", i++, dc.ColumnName, dc.DataType); // Print the column subscript, then the column's name and its data type:
            }

            DataRowCollection dra = myDataSet.Tables[tableName].Rows;
            foreach (DataRow dr in dra)
            {
                Console.WriteLine(tableName + "[{0}] is {1}", dr[0], dr[1]);// Print the CategoryID as a subscript, then the CategoryName:
            }
            Console.ReadLine();
         }
        //*****************************PLAY AREA***************************************
        #endregion PlayArea
    }
}



