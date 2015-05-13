using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Waut.PlantConfiguration.Models;
using Waut.PlantConfiguration.Services;
//using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Win32;
using System.Windows;
using System.Collections;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Data;
using System.Runtime.CompilerServices;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.PubSubEvents;


namespace Waut.Configurator.ViewModels
{
    [Export]
    public class ControlModuleViewModel : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;
        public string name;

        [Import]
        private readonly IEventAggregator eventAggregator;


        public ControlModuleViewModel()
        {
            loadFileCommand = new RelayCommand(LoadExecute);


        }

        public string Name
        {
            get { return name; }
            set
            {
               
                if(value != this.name)
                {
                    name = value;
                    NotifyPropertyChanged("Name");
                  
                }
            }
        }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if(PropertyChanged != null)
            {
                //Console.WriteLine("hoshos");
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

       // public ICollectionView service { get; set; }
      
        public List<ControlModule> GetControlModules()
        {

            ControlModuleService service = new ControlModuleService();

            //return service.GetControlModules(@"C:\Users\snel\Desktop\PLC1.xls");
            eventAggregator.ToString();
            return null;
           
        }
        //**********************
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


        
        public void LoadExecute(object obj)
        {
            //Console.WriteLine(name);
            //Console.WriteLine("Hello Model");

            //OpenFileDialog dlg = new OpenFileDialog();

            //dlg.InitialDirectory = "c:\\";
            //dlg.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            //dlg.FilterIndex = 2;
            //dlg.RestoreDirectory = true;

            //Nullable<bool> result = dlg.ShowDialog();
            ////string name;
            //if (result == true)
            //{
            //    try
            //    {
            //        name = dlg.FileName;
            //        ControlModuleService service = new ControlModuleService();
            //        service.GetControlModules(name);
            //        Console.WriteLine(name);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            //    }
            //}
    
            //var handler = this.PropertyChanged;
            //if (handler != null)
            //{
            //    handler(this,
            //          new PropertyChangedEventArgs("FavoriteColor"));
            //}
            //return name;

        }
       // ***************************
       // private readonly IEventAggregator eventAggregator;



     
    }
}
