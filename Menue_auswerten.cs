/*****************************************************************************
h e i n r i c h -h e r t z -b e r u f s k o l l e g  d e r  s t a d t  b o n n
Autor:			Meyer	
Klasse:			IA109
Datum:			22.1.2010
Datei:			Menue_auswerten.cs
Einsatz:		Funktionsdefinition
Beschreibung:	Definiert die Funktion Menue_auswerten(): wertet die Benutzereingabe
				aus und führt die entsprechende Funktion aus
Funktionen:		void Menue_auswerten(char*)
******************************************************************************
Aenderungen:
 * 28.4.2015    Umbau auf C#
*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verschluesselung
{
    partial class main
    {
        static int menue_auswerten(string value)
        {
            int auswahl = 0;
            if (value == "")
                return 0;
            if (value[0] == 'a' || value[0] == 'A')
            {
                auswahl = 1;
            }
            else if (value[0] == 'b' || value[0] == 'B')
            {
                auswahl = 2;
            }
            else if (value[0] == 'c' || value[0] == 'C')
            {
                auswahl = 3;
            }
            else if (value[0] == 'd' || value[0] == 'D')
            {
                auswahl = 4;
            }
            else if (value[0] == 'e' || value[0] == 'E')
            {
                auswahl = 5;
            }
            else
            {
                auswahl = 0;
            }
            return auswahl;
        }
    }
}