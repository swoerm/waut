using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Waut.PlantConfiguration.Models;
using Waut.PlantConfiguration.Services;
using Microsoft.Win32;
using System.Windows;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace Waut.Configurator.ViewModels
{
    public class UnitViewModel : BindableBase, INotifyPropertyChanged
    {
        List<Unit> passList = new List<Unit>();//Temp list to pass ObservableCollection to SaveControlModule

        public ObservableCollection<Unit> units;

        public UnitViewModel()
        {
            importDatabaseCommand = new RelayCommand(ImportDB);
        }


        #region ImportDatabase
        public void ImportDB(object obj)
        {
            Console.WriteLine("Hello Database ViewModel");

            OpenFileDialog dlg = new OpenFileDialog();

 

                    UnitBotecService serviceOpen = new UnitBotecService();

                    GetUnits();
                    MessageBox.Show(Name + "database is open.");
            
        }

        private ICommand importDatabaseCommand;
        public ICommand ImportDatabaseCommand
        {
            get
            {
                return importDatabaseCommand;
            }
            set
            { }
        }
        #endregion ImportFile

        #region ControlModule
        public ObservableCollection<Unit> Units
        {
            get
            {
                if (units == null)
                {
                    units = new ObservableCollection<Unit>();
                }
                return units;
            }
            set
            {
                units = value;
                NotifyPropertyChanged("Units");
            }
        }
        public void GetUnits()
        {
            UnitBotecService service = new UnitBotecService();

            Units = new ObservableCollection<Unit>(service.GetUnits("SY_ENTITY"));
            //Change OberservableCollection back to list         
            List<Unit> myList = new List<Unit>(Units);
            passList = myList;     
        }
        #endregion ControlModule

         #region Attempt EventHandler
        public event PropertyChangedEventHandler PropertyChanged;
        public string name;

        public string Name
        {
            get { return name; }
            set
            {

                if (value != this.name)
                {
                    name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion Attempt EventHandler 
    }
}

        
