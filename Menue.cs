/*****************************************************************************
h e i n r i c h -h e r t z -b e r u f s k o l l e g  d e r  s t a d t  b o n n
Autor:			Meyer	
Klasse:			IA109
Datum:			22.1.2010
Datei:			Menue.cs
Einsatz:		Funktionsdefinition
Beschreibung:	Definiert die Funktion Menue(): nimmt einen Menuetext entgegen
				und liefert die Auswahl des Benutzers zurück
Funktionen:		char* Menue(char*)
******************************************************************************
Aenderungen:
29/05/2011  erweitert auf ASCII 254
28.4.2015   Umbau auf C#
*****************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verschluesselung
{
    partial class main
    {
        static string menue(string Pfad)
        {
        	FileStream Datei = new FileStream(Pfad,FileMode.Open);
            StreamReader SR = new StreamReader(Datei);
            string ergebnis = null;
            string hilf = null;
        	//Menue laden
        	
        	if(!Datei.CanRead)
        	{
        		Console.WriteLine("\n\n Menuetext nicht verfuegbar!\n");
                return null;
        	}
        	else
        	{
        
        		while((hilf = SR.ReadLine()) != null)
                {
                    ergebnis += hilf;
                    ergebnis += "\n";
                }
        	}

        	//Menue anzeigen
        	Console.WriteLine(ergebnis);
        
        	//Eingabeaufforderung
        	Console.WriteLine("\n\nIhre Auswahl ist? ");
        	ergebnis = Console.ReadLine();

            SR.Close();
            Datei.Close();
        	return ergebnis;
        }
    }
}