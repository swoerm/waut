using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waut.PlantConfiguration.Models;
using Waut.PlantConfiguration.Services;

namespace Waut.Configurator.ViewModels
{
    public class ControlModuleViewModel
    {

        public List<ControlModule> GetControlModules()
        {

            ControlModuleService service = new ControlModuleService();

            return service.GetControlModules("");
        }

    }
}
