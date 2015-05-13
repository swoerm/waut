using System.Collections.Generic;
using Waut.PlantConfiguration.Models;


namespace Waut.PlantConfiguration.Services
{
    interface IControlModuleService 
    {
        List<ControlModule> GetControlModules(string FileName);
        
    }
}
