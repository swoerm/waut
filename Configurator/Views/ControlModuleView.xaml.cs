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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Waut.Configurator.ViewModels;
using System.Collections;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;

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
            
        }

        /// <summary>
        /// Sets the ViewModel.
        /// </summary>
        /// <remarks>
        /// This set-only property is annotated with the <see cref="ImportAttribute"/> so it is injected by MEF with
        /// the appropriate view model.
        /// </remarks>
        [Import]
        [SuppressMessage("Microsoft.Design", "CA1044:PropertiesShouldNotBeWriteOnly", Justification = "Needs to be a property to be composed by MEF")]
        ControlModuleViewModel ViewModel
        {
            set
            {
                this.DataContext = value;

                ControlModuleViewModel x = DataContext as ControlModuleViewModel;

                CMGrid.ItemsSource = x.GetControlModules();
            }
        } 
    }
}
