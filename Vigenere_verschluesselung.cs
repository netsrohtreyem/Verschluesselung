/*****************************************************************************
h e i n r i c h -h e r t z -b e r u f s k o l l e g  d e r  s t a d t  b o n n
Autor:			Meyer	
Klasse:			IA109
Datum:			22.1.2010
Datei:			Vigenere_verschluesselung.cs
Einsatz:		Funktionsheader
Beschreibung:	Definiert die Funktion Vigenere_verschluesselung()
				Verschlüsselt:  ASCII 10 + 13 + ( 32 -> 254)
******************************************************************************
Aenderungen:
05/06/2010	Ascii erweitert um ASCII 10/13 Zeilenumbruch CR/LF
29/05/2011  erweitert auf ASCII 254
17.6.2013   erweitert auf ASCII 0 - 255
30.4.2015   Umbau auf C#
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
        static string Vigenere_verschluesselung(string Quelle, int anzahl, string Schluessel)
        {
        	StringBuilder Geheim = new StringBuilder(Quelle,anzahl);
        	int index = 0;
        	int index2 = 0;
        	int temp = 0;
        	//Alle Zeichen verschlüsseln
        	for(index = 0;index<anzahl-1;index++)
        	{
        		index2 = 0;
        		//Alle Schlüsselstellen verarbeiten (Achtung Schlüssel muss terminiert sein!
        		while(index2<Schluessel.Length && index < anzahl)
        		{
        			int temp2 = ((int)Quelle[index] + (int)(Schluessel[index2]));
        			//Wenn Überlauf, dann 255 abziehen
        			if(temp2 > 255)
        			{
        				temp = ((int)(Quelle[index]) + (int)(Schluessel[index2]));
        				Geheim[index] = (char)(temp - 255);
        			}
        			else
        			{
        				Geheim[index] = (char)((int)Quelle[index] + (int)Schluessel[index2]);
        			}
        			index2++;
        			index++;
        		}
        		if(index>0)
        		{
        			index--;//Einer zurück da Kopfgesteuert!
        		}
        		else
        		{
        			//nichts
        		}
        	}
        	return Geheim.ToString();
        }
    }
}