using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waut.PlantConfiguration.Models;


namespace Waut.PlantConfiguration.Services
{
    interface IControlModuleService 
    {
        List<ControlModule> GetControlModules(string FileName);
        
    }
}
