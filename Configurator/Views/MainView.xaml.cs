using Waut.Configurator.ViewModels;
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


namespace Waut.Configurator.Views
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
        void OnClick1(object sender, RoutedEventArgs e)
        {
            ManageData.Background = Brushes.LightBlue;
            MessageBox.Show("Call Data");
        }
    }
}
