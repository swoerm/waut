using Configurator.ViewModels.MainViewModel;
using Microsoft.Practices.Prism.Mvvm;
using System;
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
using System.Windows.Shapes;
using Configurator.Waut.Model.PlantConfiguration;
using Configurator.Waut.Servers;

namespace Configurator.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window, IView
    {
        public MainView()
        {
            InitializeComponent();
            MainViewModel x = DataContext as MainViewModel;
            //ExcelRead();
        }

    }
}
