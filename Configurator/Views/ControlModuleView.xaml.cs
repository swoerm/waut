﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Waut.Configurator.ViewModels;

namespace Waut.Configurator.Views
{
    /// <summary>
    /// Interaction logic for ControlModuleView.xaml
    /// </summary>
    public partial class ControlModuleView : UserControl
    {
        public ControlModuleView()
        {
            InitializeComponent();
            ControlModuleViewModel x = DataContext as ControlModuleViewModel;

            CMGrid.ItemsSource = x.GetControlModules();
        }
    }
}