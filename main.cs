/*****************************************************************************
h e i n r i c h -h e r t z -b e r u f s k o l l e g  d e r  s t a d t  b o n n
Autor:			Meyer	
Klasse:			IA214
Datum:			21.4.2015
Datei:			main.cs
Einsatz:		Hauptprogramm
Beschreibung:	Ver- bzw. Entschlüsselt Textdateien (mit Caesar,Vigenere oder 
				One Time Pad
******************************************************************************
Aenderungen:
21.4.2015       Entwicklung begonnen, umschreiben von C++ -> C#
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
        static void Main(string[] args)
        {
	        //Zentrale Daten
	        string Quelltext  = "";
	        string Geheimtext = "";
	        string Schluessel = "";

	        //Splash
	        splashscreen("splashinfo.txt");

	        //Hauptschleife
	        run(ref Quelltext,ref Geheimtext,ref Schluessel);

	        //Datensichern
            //entfällt
        }
    }
}
