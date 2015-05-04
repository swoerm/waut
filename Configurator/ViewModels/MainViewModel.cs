﻿using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Windows;
using System.IO;

using Microsoft.Win32;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
//using System.Windows.Forms;
using System.Data;
//using Waut.PlantConfiguration.Services.ControlModuleService;
using Waut.Configurator.ViewModels;
using Microsoft.Practices.Prism.Mvvm;
using Waut.PlantConfiguration.Models;
using Waut.PlantConfiguration.Services;
using System.Collections.Generic;

namespace Waut.Configurator.ViewModels
{
    public class RelayCommand : ICommand
    {
      
        private Action<object> execute;

        private Predicate<object> canExecute;

        private event EventHandler CanExecuteChangedInternal;

        public RelayCommand(Action<object> execute)
            : this(execute, DefaultCanExecute)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            

            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            if (canExecute == null)
            {
                throw new ArgumentNullException("canExecute");
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                this.CanExecuteChangedInternal += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
                this.CanExecuteChangedInternal -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute != null && this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
            
            //ExcelRead();
        }

        public void OnCanExecuteChanged()
        {
            EventHandler handler = this.CanExecuteChangedInternal;
            if (handler != null)
            {
                //DispatcherHelper.BeginInvokeOnUIThread(() => handler.Invoke(this, EventArgs.Empty));
                handler.Invoke(this, EventArgs.Empty);
            }
        }

        public void Destroy()
        {
            this.canExecute = _ => false;
            this.execute = _ => { return; };
        }

        private static bool DefaultCanExecute(object parameter)
        {
            return true;
        }
    }
    public class MainViewModel : BindableBase, INotifyPropertyChanged
    {
        private ICommand hiButtonCommand;
       // private ICommand readDataCommand;
        private ICommand loadFileCommand;
        private ICommand toggleExecuteCommand { get; set; }

        private bool canExecute = true;

        public string HiButtonContent
        {
            get
            {
                return "click to hi";
            }
        }

        public bool CanExecute
        {
            get
            {
                return this.canExecute;
            }

            set
            {
                if (this.canExecute == value)
                {
                    return;
                }

                this.canExecute = value;
            }
        }

        public ICommand ToggleExecuteCommand
        {
            get
            {
                return toggleExecuteCommand;
            }
            set
            {
                toggleExecuteCommand = value;
            }
        }

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

        public ICommand HiButtonCommand
        {
            get
            {
                return hiButtonCommand;
            }
            set
            {
                hiButtonCommand = value;
            }
        }

        public MainViewModel()
        {
            HiButtonCommand = new RelayCommand(ShowMessage, param => this.canExecute);
            toggleExecuteCommand = new RelayCommand(ChangeCanExecute);
            loadFileCommand = new RelayCommand(LoadExecute);
            
        }

        public void ShowMessage(object obj)
        {
            MessageBox.Show(obj.ToString());
        }

        public void ChangeCanExecute(object obj)
        {
            canExecute = !canExecute;
        }

        //public List<ControlModule> GetControlModules()
        //{

        //    ControlModuleService service = new ControlModuleService();

        //    return service.GetControlModules(@"C:\Users\snel\Desktop\PLC1.xls");
        //}
        public void LoadExecute(object obj)
        {
            Console.WriteLine("Hello Model");

            OpenFileDialog dlg = new OpenFileDialog();

            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dlg.FilterIndex = 2;
            dlg.RestoreDirectory = true;

            Nullable<bool> result = dlg.ShowDialog();
            string name;
            if (result == true)
            {
                try
                {
                    name = dlg.FileName;
                    ControlModuleService service = new ControlModuleService();
                    service.GetControlModules(name);
                    Console.WriteLine(name);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            //return name;

        }

        public string ButtonContent
        {
            get
            {
                return "Click Me";
            }

          
        }
    }
}
