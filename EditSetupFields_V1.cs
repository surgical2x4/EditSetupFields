using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
//Script usage:
// Start with 3 blank setup fields (CTRL F9 x3), then run script (ideally with shortcut).
// The default field names of setup fields are "Field 1", etc. Script looks for these,
// changes the name, field size, and applies DRR. This allows the tolerance table to be set initially (assuming your treatment fields have been set already),
// rather than creating setup fields in script (which cannot set tolerance table).
// The script will not/cannot set gantry angle (e.g., 270 for Rt Lat Setup). Thist must be changed manually before or after.
// Found to be marginally faster to run this with 3 CTRL F9 inputs, Shortcut to run script, SPACE (to accept save window), then change G0 to G270 manually, rather than run
// a script that creates setupfields (utilizing AddSetupFields) and add a tolerance table manually, after running.

//Changelog:
//
//

[assembly: ESAPIScript(IsWriteable = true)]

namespace VMS.TPS
{
    public class Script
    {
        public Script()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Execute(ScriptContext context /*, System.Windows.Window window, ScriptEnvironment environment*/)
        {
            // TODO : Add here the code that is called when the script is launched from Eclipse.
            //Check if plan exists
            if (context.PlanSetup == null)
            {
                throw new ApplicationException("No plan loaded.");
            }

            PlanSetup plan = context.PlanSetup;
            Patient patient = context.Patient;

            //DRR settings
            var myDRR = new DRRCalculationParameters(500); // 500 mm is the DRR size
            myDRR.SetLayerParameters(0, 1, 100, 1000); //Bones.dps 

            //Loop through all fields in the plan
            foreach (Beam beam in plan.Beams)
            {
                //Check if field is a setup field
                if (beam.IsSetupField)
                {
                    //Check the field name and then change it accordingly
                    if (beam.Id == "Field 1")
                    {
                        patient.BeginModifications();
                        
                        //Adjust the jaw size to 30x16 cm2
                        BeamParameters param = beam.GetEditableParameters();
                        param.SetJawPositions(new VRect<double>(-150, -80, 150, 80));
                        beam.ApplyParameters(param);
                        
                        //Change the name of Field 1 to CBCT
                        beam.Id = string.Format("CBCT");
                    }
                    else if (beam.Id == "Field 2")
                    {
                        patient.BeginModifications();
                        
                        //Adjust the jaw size to 15x15 cm2
                        BeamParameters param = beam.GetEditableParameters();
                        param.SetJawPositions(new VRect<double>(-75, -75, 75, 75));
                        beam.ApplyParameters(param);
                        
                        //Change the name of Field 2 to AP Setup
                        beam.Id = string.Format("AP Setup");
                        
                        //Add the DRR
                        beam.CreateOrReplaceDRR(myDRR);
                    }
                    else if (beam.Id == "Field 3")
                    {
                        patient.BeginModifications();
                       
                        //Adjust the jaw size to 15x15 cm2
                        BeamParameters param = beam.GetEditableParameters();
                        param.SetJawPositions(new VRect<double>(-75, -75, 75, 75));
                        beam.ApplyParameters(param);
                        
                        //Change the name of Field 2 to AP Setup
                        beam.Id = string.Format("Rt Lat Setup");
                        
                        //Add the DRR
                        beam.CreateOrReplaceDRR(myDRR);
                    }
                }
            }
        }
    }
}
