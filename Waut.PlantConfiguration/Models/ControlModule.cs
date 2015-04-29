using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;


namespace Waut.PlantConfiguration.Models
{
    public class ControlModule
    {
        public ControlModule()
        {

        }
        
        public string Description { get; set; }

        public int Number { get; set; }

        public string Type { get; set; }

        public string UoM { get; set; }

        public string Format { get; set; }

        public string KronesSymbolism { get; set; }

        public string ProjectSymbolism { get; set; }

        //nie nodig
        public string Symbol1 { get; set; }

        public string Symbol2 { get; set; }

        public string Symbol3 { get; set; }

        public string Symbol4 { get; set; }
    }

}
