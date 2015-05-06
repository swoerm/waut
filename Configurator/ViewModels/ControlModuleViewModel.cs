﻿using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input; 
using Waut.PlantConfiguration.Models;
using Waut.PlantConfiguration.Services;
//using Waut.Configurator.ViewModels;
//using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Win32;
using System.Windows;
using System.Collections;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Data;
using System.Runtime.CompilerServices;



namespace Waut.Configurator.ViewModels
{
    public class ControlModuleViewModel : BindableBase, INotifyPropertyChanged
    {

        public ControlModuleViewModel(/*ObservableCollection<ControlModuleService> service*/)
        {
            importFileCommand = new RelayCommand(ImportX);
            bindToList = new RelayCommand(ImportX);
        }

        public string Name;
        public void ImportX(object obj)
        {          
            Console.WriteLine("Hello Model");

            OpenFileDialog dlg = new OpenFileDialog();

            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dlg.FilterIndex = 2;
            dlg.RestoreDirectory = true;

            Nullable<bool> result = dlg.ShowDialog();
            
            if (result == true)
            {
                try
                {
                    Name = dlg.FileName;
                    ControlModuleService service = new ControlModuleService(); //Add binding here
                    service.GetControlModules(Name);

                    Console.WriteLine(Name);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private ICommand importFileCommand;
        private ICommand bindToList;
        
        private ICommand BindToList
        {
            get
            {
                return bindToList;

            }
            set
            { }
        }
        public ICommand ImportFileCommand
        {
            get
            {
                return importFileCommand;
            }
            set
            {}
        }
        public List<ControlModule> GetControlModules()
        {

            ControlModuleService service = new ControlModuleService();

            //ImportX;

            return service.GetControlModules(@"C:\Users\snel\Desktop\PLC1.xls");
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        //public string name;

        //public string Name
        //{
        //    get { return name; }
        //    set
        //    {

        //        if (value != this.name)
        //        {
        //            name = value;
        //            NotifyPropertyChanged("Name");
        //        }
        //    }
        //}
        //private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}

        private ICommand loadFileCommand;
        public ICommand ReadDataCommand
        {

            get
            {
                return loadFileCommand;
            }
            set
            {
                loadFileCommand = value;
                Console.WriteLine("Hello Model");
            }
        }
       
    }
}
