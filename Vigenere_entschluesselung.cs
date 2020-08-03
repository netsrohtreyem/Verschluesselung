/*****************************************************************************
h e i n r i c h -h e r t z -b e r u f s k o l l e g  d e r  s t a d t  b o n n
Autor:			Meyer	
Klasse:			IA109
Datum:			22.1.2010
Datei:			Vigenere_entschluesselung.cpp
Einsatz:		Funktionsheader
Beschreibung:	Definiert die Funktion Vigenere_entschluesselung()

******************************************************************************
Aenderungen:
05/06/2010	Ascii erweitert um ASCII 10/13 Zeilenumbruch CR/LF
29/05/2011  erweitert auf ASCII 254
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
        static string Vigenere_entschluesselung(string Quelle, int anzahl, string Schluessel)
        {
            StringBuilder encrypt = new StringBuilder(Quelle, anzahl);
        	int index = 0;
        	int index2 = 0;
        	//Alle Zeichen verschlüsseln
        	for(index = 0;index<anzahl-1;index++)
        	{
        		index2 = 0;
        		//Alle Schlüsselstellen verarbeiten (Achtung Schlüssel muss terminiert sein!
        		while(index2 < Schluessel.Length && index < anzahl)
        		{
        			int temp = (int)Quelle[index] - (int)(Schluessel[index2]);
        			//wenn Überlauf, dann 255 hinzurechnen
        			if(temp < 0)
        			{
        				encrypt[index] = (char)(255 + (int)Quelle[index] - (int)Schluessel[index2]);
        			}
                    else if (temp == 10)
                    {
                        encrypt[index] = '\n';
                    }
                    else if (temp == 13)
                    {
                        encrypt[index] = '\r';
                    }
                    else
        			{
        				encrypt[index] = (char)((int)Quelle[index] - (int)Schluessel[index2]);
        			}
        			index2++;
        			index++;
        		}
        		//Beim Schlüsselindex index2 wieder von vorn beginnen
        		if(index > 0)
        		{
        			index--; //Einer zurück da Kopfgesteuert!
        		}
        		else
        		{
        			//nichts	
        		}
        	}
        	return encrypt.ToString();
        }
    }
}