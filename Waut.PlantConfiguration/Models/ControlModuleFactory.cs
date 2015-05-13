using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waut.PlantConfiguration.Models
{
    public class ControlModuleFactory
    {
        public static ControlModule CreateControlModuleXLS(string[] arr)
        {
            ControlModule cm = null;
            
            int inByte;
            bool isNumericIn = Int32.TryParse(arr[22], out inByte);
            
            int outByte;
            bool isNumericOut = Int32.TryParse(arr[11], out outByte);


            if (arr[6] != "" && arr[6] != "DESCRIPTION")
            {
                //Console.WriteLine(isNumericIn + "   " + isNumericOut);
                //Console.WriteLine(" IN: " + inByte + " OUT: " + outByte);
                if (outByte < Botec.ActuatorIORange && inByte < Botec.ActuatorIORange)
                {
                    cm = new Actuator();
                }
                else if (inByte >= Botec.ActuatorIORange && inByte < Botec.SpecialInputIORange)
                {
                    cm = new SpecialInput();
                }
                else if (inByte >= Botec.SpecialInputIORange && inByte < Botec.AnalogIORange)
                {
                    cm = new AnalogInput();
                }
                else if (outByte >= Botec.AnalogIORange && outByte < Botec.VLTIORange)
                {
                    cm = new AnalogOutput();
                }
                else if (inByte >= Botec.VLTIORange)
                {
                    cm = new VLT();
                }

                if (cm != null)
                {
                    cm.Description = arr[6];
                    cm.Type = arr[8];
                    cm.UoM = arr[1];
                    cm.Format = arr[4];
                    cm.Symbol1 = arr[2];
                    cm.Symbol2 = arr[3];
                    cm.Symbol3 = arr[4];
                    cm.Symbol4 = arr[5];

                }
            }
            
            return cm;
        }
    }
}
