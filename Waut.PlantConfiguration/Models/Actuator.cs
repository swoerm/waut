using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waut.PlantConfiguration.Models
{
    public class Actuator : ControlModule   
    {
        
        public string KronesSymbol
        {
            get
            {
                var s = "";
                int first;
                //bool isNum = isNum;
                bool lekker = string.IsNullOrEmpty(Symbol1);
                Console.WriteLine(lekker);
                bool isNumeric = int.TryParse(Symbol1.Substring(0, 1), out first);
                if (isNumeric == true)
                {
                    s = "x" + Symbol1 + Symbol2 + Symbol3 + Symbol4;
                }
                else
                {
                    s = Symbol1 + Symbol2 + Symbol3 + Symbol4;
                }
                return s;  
            }
        }
    }
}
