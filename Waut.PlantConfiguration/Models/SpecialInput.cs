using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waut.PlantConfiguration.Models
{
    class SpecialInput : ControlModule
    {
        public string KronesSymbol
        {
            get
            {
                var s = "";
                int first;
                    bool isEmpty = string.IsNullOrEmpty(Symbol1);
                    bool isNumeric = Int32.TryParse(Symbol1.Substring(0, 1), out first);

                    if (isNumeric == true && isEmpty != true)
                    {
                        s = "x" + Symbol1 + Symbol2 + Symbol3 + Symbol4;
                    }
                    else if (isNumeric == true && isEmpty != true)
                    {
                        s = Symbol1 + Symbol2 + Symbol3 + Symbol4;
                    }
                
            return s; 
            }
        }
    }
}
