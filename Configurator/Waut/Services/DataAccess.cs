using System;
using System.Data;
using System.Data.OleDb;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configurator.Waut.Services
{
    public class MainClass
    {
        public static void AccessRead()
        {
            // Set Access connection and select strings.
            // The path to BugTypes.MDB must be changed if you build the sample from the command line:
#if USINGPROJECTSYSTEM
		string strAccessConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=..\\..\\BugTypes.MDB";
#else
            string strAccessConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=BugTypes.MDB";
#endif
            string strAccessSelect = "SELECT * FROM Categories";

            // Create the dataset and add the Categories table to it:
            DataSet myDataSet = new DataSet();
            OleDbConnection myAccessConn = null;
            try
            {
                myAccessConn = new OleDbConnection(strAccessConn);
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
                myDataAdapter.Fill(myDataSet, "Categories");

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

            // A dataset can contain multiple tables, so let's get them all
            // into an array:
            DataTableCollection dta = myDataSet.Tables;
            foreach (DataTable dt in dta)
            {
                Console.WriteLine("Found data table {0}", dt.TableName);
            }

            // The next two lines show two different ways you can get the
            // count of tables in a dataset:
            Console.WriteLine("{0} tables in data set", myDataSet.Tables.Count);
            Console.WriteLine("{0} tables in data set", dta.Count);
            // The next several lines show how to get information on a specific table by name from the dataset:
            Console.WriteLine("{0} rows in Categories table", myDataSet.Tables["Categories"].Rows.Count);
            // The column info is automatically fetched from the database, so we can read it here:
            Console.WriteLine("{0} columns in Categories table", myDataSet.Tables["Categories"].Columns.Count);
            DataColumnCollection drc = myDataSet.Tables["Categories"].Columns;
            int i = 0;
            foreach (DataColumn dc in drc)
            {
                // Print the column subscript, then the column's name and its data type:
                Console.WriteLine("Column name[{0}] is {1}, of type {2}", i++, dc.ColumnName, dc.DataType);
            }
            DataRowCollection dra = myDataSet.Tables["Categories"].Rows;
            foreach (DataRow dr in dra)
            {
                // Print the CategoryID as a subscript, then the CategoryName:
                Console.WriteLine("CategoryName[{0}] is {1}", dr[0], dr[1]);
            }
        }

        public static void ExcelRead()
        {
            string con = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=MovieNames.xlsx;Extended Properties='Excel 8.0;HDR=Yes;'";
            using (OleDbConnection connection = new OleDbConnection(con))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("select * from [Sheet1$]", connection);
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
        }
        public void SQLRead()
        {
            SqlConnection conn = new SqlConnection("Server=.\\SQLEXPRESS; Database=hello;Integrated Security=true");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT Item1, Item2 FROM [Name1].[Name2]", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("{1}, {0}", reader.GetString(0), reader.GetString(1));
            }
            reader.Close();
            conn.Close();
            //Delay exit
            if (Debugger.IsAttached)
            {
                Console.ReadLine();
            }
        }
    }

}
