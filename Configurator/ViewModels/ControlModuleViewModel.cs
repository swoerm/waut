using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waut.PlantConfiguration.Models;
using Waut.PlantConfiguration.Services;
//using Microsoft.Practices.Prism.PubSubEvents;

namespace Waut.Configurator.ViewModels
{
    public class ControlModuleViewModel: BindableBase
    {
        //private readonly IEventAggregator eventAggregator;
        public List<ControlModule> GetControlModules()
        {

            ControlModuleService service = new ControlModuleService();

            return service.GetControlModules(@"C:\Users\snel\Desktop\PLC1.xls");
        }

    }
}
