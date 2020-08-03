/*****************************************************************************
h e i n r i c h -h e r t z -b e r u f s k o l l e g  d e r  s t a d t  b o n n
Autor:			Meyer	
Klasse:			IA109
Datum:			22.1.2010
Datei:			One_time_pad_verschluesselung.cs
Einsatz:		Funktionsheader
Beschreibung:	Definiert die Funktion One_time_pad_verschluesselung()

******************************************************************************
Aenderungen:
05/06/2010	Ascii erweitert um ASCII 10/13 Zeilenumbruch CR/LF
29/05/2011  erweitert auf ASCII 254
4.5.2015 Umbau auf C#
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
        static string One_time_pad_verschluesselung(ref string Quelle, int anzahl, ref string Schluessel)
        {
            StringBuilder Geheim = new StringBuilder(Quelle);
        	int index = 0;
        	int temp = 0;
        	//Alle Zeichen verschlüsseln
        	for(index = 0;index<anzahl-1;index++)
        	{
        	    int temp2 = ((int)Quelle[index] + (int)Schluessel[index]);
        	    //Wenn Überlauf, dann 255 abziehen
        	    if(temp2 > 255)
        	    {
        	    	temp = ((int)(Quelle[index]) + (int)(Schluessel[index]));
        	    	Geheim[index] = (char)(temp - 255);
        	    }
        	    else
        	    {
        	    	Geheim[index] = (char)((int)Quelle[index] + (int)Schluessel[index]);
        	    }
        	}
        	return Geheim.ToString();
        }
    }
}