using Configurator.Waut.Model.PlantConfiguration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configurator.Waut.Service
{
    class ExcelDataService
    {
        OleDbConnection Conn;
        OleDbCommand Cmd;

        public ExcelDataService()
        {
            string ExcelFilePath = @"H:\\SchoolManagement.xlsx";
            string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ExcelFilePath + ";Extended Properties=Excel 12.0;Persist Security Info=True";
            Conn = new OleDbConnection(excelConnectionString);
        }

        /// <summary>  
        /// Method to Get All the Records from Excel  
        /// </summary>  
        /// <returns></returns>  
        public async Task<ObservableCollection<ControlModule>> ReadRecordFromEXCELAsync()
        {
            ObservableCollection<ControlModule> ControlModules = new ObservableCollection<ControlModule>();
            await Conn.OpenAsync();
            Cmd = new OleDbCommand();
            Cmd.Connection = Conn;
            Cmd.CommandText = "Select * from [Sheet1$]";
            var Reader = await Cmd.ExecuteReaderAsync();
            while (Reader.Read())
            {
                ControlModules.Add(new ControlModule()
                {
                    StudentID = Convert.ToInt32(Reader["StudentID"]),
                    Name = Reader["Name"].ToString(),
                    Email = Reader["Email"].ToString(),
                    Class = Reader["Class"].ToString(),
                    Address = Reader["Address"].ToString()
                });
            }
            Reader.Close();
            Conn.Close();
            return ControlModules;
        }  
    }
}
