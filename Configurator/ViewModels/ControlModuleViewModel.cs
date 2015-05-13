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
    public class ControlModuleViewModel : BindableBase, INotifyPropertyChanged
    {
        List<ControlModule> passList = new List<ControlModule>();//Temp list to pass ObservableCollection to SaveControlModule

        public ObservableCollection<ControlModule> controlModules;

        public ControlModuleViewModel()
        {
            importFileCommand = new RelayCommand(ImportX);
            exportFileCommand = new RelayCommand(ExportX);
        }

        #region ImportFile
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
                    GetControlModules();
                    Console.WriteLine(Name);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private ICommand importFileCommand;
        public ICommand ImportFileCommand
        {
            get
            {
                return importFileCommand;
            }
            set
            { }
        }
        #endregion ImportFile

        #region ExportFile

        private ICommand exportFileCommand;
        public ICommand ExportFileCommand
        {
            get
            {
                return exportFileCommand;
            }
            set
            { }
        }
        public void ExportX(object obj)
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

                    ControlModuleBotecService serviceSave = new ControlModuleBotecService();

                    serviceSave.SaveControlModule(passList, Name, "NewModule");
                    MessageBox.Show("Data saved to " + Name);

                    Console.WriteLine(Name);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not write file to disk. Original error: " + ex.Message);
                }
            }
        }
        #endregion ExportFile

        #region ControlModule
        public ObservableCollection<ControlModule> ControlModules
        {
            get
            {
                if (controlModules == null)
                {
                    controlModules = new ObservableCollection<ControlModule>();
                }
                return controlModules;
            }
            set
            {
                controlModules = value;
                NotifyPropertyChanged("ControlModules");
            }
        }
        public void GetControlModules()
        {
            ControlModuleService service = new ControlModuleService();

            ControlModules = new ObservableCollection<ControlModule>(service.GetControlModules(Name));
            //Change OberservableCollection back to list         
            List<ControlModule> myList = new List<ControlModule>(ControlModules);
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