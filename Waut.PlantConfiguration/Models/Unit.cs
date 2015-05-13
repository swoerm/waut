using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Waut.PlantConfiguration.Models
{
    public class Unit :  ObservableCollection<Unit>
    {
      
        public Unit()
        {

        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Type { get; set; }
        public string KronesSectorSymbol { get; set; }
        public string ProjectSectorSymbol { get; set; }
        public string KronesSymbol { get; set; }
        public string ProjectSymbol { get; set; }
        public string PLCID { get; set; }
        public string Description { get; set; }
        public string ClassID { get; set; }
        public List<ControlModule> ControlModules { get; set; }
    }
}


