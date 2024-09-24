Alternative version to creating setup fields quickly in Eclipse rather than using AddSetupField. 
Have fields already present in the plan with a tolerance table assigned.

Keystrokes: CTRL F9 (x3), Assigned shortcut (e.g., CTRL R), SPACE, Change G0 -> G270. ~6 second process.

Copied from script header:
//Script usage:
Start with 3 blank setup fields (CTRL F9 x3), then run script (ideally with shortcut).
The default field names of setup fields are "Field 1", etc. Script looks for these,
changes the name, field size, and applies DRR. This allows the tolerance table to be set initially (assuming your treatment fields have been set already),
rather than creating setup fields in script (which cannot set tolerance table).
The script will not/cannot set gantry angle (e.g., 270 for Rt Lat Setup). Thist must be changed manually before or after.
Found to be marginally faster to run this with 3 CTRL F9 inputs, Shortcut to run script, SPACE (to accept save window), then change G0 to G270 manually, rather than run
a script that creates setupfields (utilizing AddSetupFields) and add a tolerance table manually, after running.
