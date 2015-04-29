using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waut.PlantConfiguration.Models;

namespace Waut.PlantConfiguration.Services
{
    public class ControlModuleService : IControlModuleService
    {
        public string FileName { get; set; }

        public ControlModuleService()
        {

        }

        public List<ControlModule> GetControlModules(string FileName)
        {
            List<ControlModule> list = new List<ControlModule>();



            ControlModule cm = new ControlModule();
            cm.Description = "af as asd as ";





            list.Add(cm);


            return list;
        }
    }
}
