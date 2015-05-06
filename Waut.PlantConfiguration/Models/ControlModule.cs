using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Collections.ObjectModel;



namespace Waut.PlantConfiguration.Models
{
    public class ControlModule : ObservableCollection<ControlModule>
    {
        public ControlModule()
        {

        }

        public bool IsValid()
        {
            //return Symbol1
            return false;
        }
        
        public string Description { get; set; }

        public int Number { get; set; }//int

        public string Type { get; set; }

        public string UoM { get; set; }

        public string Format { get; set; }

        public string KronesSymbol { get; set; }

        public string ProjectSymbol { get; set; }

        public string Symbol1 { get; set; }

        public string Symbol2 { get; set; }//int

        public string Symbol3 { get; set; }

        public string Symbol4 { get; set; }//int

        public string Sheet { get; set; }
        public string File { get; set; }
    }

}
