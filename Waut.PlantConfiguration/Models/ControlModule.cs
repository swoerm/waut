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
            return false;
        }
        
        public string Description { get; set; }
        public int Number { get; set; }
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
