/*****************************************************************************
h e i n r i c h -h e r t z -b e r u f s k o l l e g  d e r  s t a d t  b o n n
Autor:			Meyer	
Klasse:			IA109
Datum:			22.1.2010
Datei:			run.cs
Einsatz:		Funktionsdefinition
Beschreibung:	Deklariert die Funktion run() incl. Hauptschleife des Verschlüsselungsprogramms
Funktionen:		void run()
******************************************************************************
Aenderungen:
28.4.2015   Umbau auf C#
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
        static void run(ref string quelle,ref string ziel,ref string schluessel)
        {
	        //lokale Daten
	        int auswahl = 0;
	        string Auswahl_text = null;
	        //Hautpprogrammschleife
	        do
	        {
	        	//Menue anzeigen und Auswahl entgegennehmen
	        	Auswahl_text = menue("Menue.txt");
                if (Auswahl_text == null || Auswahl_text == "")
                    continue;
	        	//Auswahl auswerten
	        	auswahl = menue_auswerten(Auswahl_text);

                //Menuepunkt ausführen (optional auch als eigene Funktion!)
	        	switch(auswahl)
	        	{
	        		case 0:
	        			break;
	        		case 1:	
	        			ziel = Datei_verschluesseln(ref quelle,ref schluessel);
                        Console.ReadLine();
	        			if(ziel == null)
	        			{
	        				continue;
	        			}
	        			break;
	        		case 2:				
	        			ziel = Datei_entschluesseln(ref quelle,ref schluessel);
                        if (ziel == null)
                            break;
                        //Console.ReadLine();
	        			if(ziel == null)
	        			{
	        				continue;
	        			}
	        			break;
	        		case 5:
	        			auswahl = 0;
	        			continue;
	        		default:
	        			//Fehler 1 -> falschen Menüpunkt ausgewählt
	        			break;
	        	}
	        }while(auswahl !=0); //Wiederholen bis Menüpunkt 0 -> "Ende" ausgeählt wird
        }
    }
}