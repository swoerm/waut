using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Waut.PlantConfiguration.Models;

namespace Waut.PlantConfiguration.Services
{
    public class ControlModuleBotecService
    {
        public void SaveControlModule(List<ControlModule> pastList, string fileName, string tableName)
        {

#if         USINGPROJECTSYSTEM  // Set Access connection and select strings.
            string con = @"Provider=Microsoft.JET.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties='Excel 8.0;HDR=Yes;'";
#else
            string conn = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName;
#endif
            string strAccessSelect = "SELECT * FROM " + tableName;

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
                    //Still need to add 'number' to database
                    string strSQL = "INSERT INTO NewModule (Description, Type, UoM, Format, File) " + "VALUES ( '" + pastList[j].Description + "' , '" +
                    pastList[j].Type + "' ,'" + pastList[j].UoM + "' ,'" + pastList[j].Format + "','" + pastList[j].File + "')";
                    addObj = new OleDbCommand(strSQL, myAccessConn);
                    addObj.ExecuteNonQuery();

                    string strNumSQL = "INSERT INTO NewModule (Number) " + "VALUES ( '" + pastList[j].Number + "')";
                    addObj = new OleDbCommand(strNumSQL, myAccessConn);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Failed to retrieve the required data from the DataBase.\n{0}", ex.Message);
                Console.WriteLine(ex.ToString());
                return;
            }
            finally
            {
                myAccessConn.Close();
            }
        }

    }
}
