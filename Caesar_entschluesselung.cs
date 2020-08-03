/*****************************************************************************
h e i n r i c h -h e r t z -b e r u f s k o l l e g  d e r  s t a d t  b o n n
Autor:			Meyer	
Klasse:			IA109
Datum:			22.1.2010
Datei:			Caesar_entschluesselung.cpp
Einsatz:		Funktionsheader
Beschreibung:	Definiert die Funktion Caesar_entschluesselung()

******************************************************************************
Aenderungen:
05/06/2010	Ascii erweitert um ASCII 10/13 Zeilenumbruch CR/LF
29/05/2011  erweitert auf ASCII 254
17/06/2013  ASCII 0  - 255
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
        static string Caesar_entschluesselung(string Quelle, int anzahl, string Schluessel)
        {
            String encrypt = "";
            char encryptchar;
        	int index=0;
        	//Alle Zeichen entschlüsseln
        	for(index = 0;index<anzahl;index++)
        	{
        		int temp = (int)Quelle[index] - ((int)Schluessel[0]);
        		//wenn Überlauf, dann 255 hinzurechnen
        		if(temp < 0)
        		{
        			encryptchar = (char)(255 + (int)Quelle[index] - (int)Schluessel[0]);
        		}
                else if (temp == 10)
                {
                    encryptchar = '\n';
                }
                else if (temp == 13)
                {
                    encryptchar = '\r';
                }
                else
                {
                    encryptchar = (char)((int)Quelle[index] - (int)Schluessel[0]);
                }
                encrypt += encryptchar;
        	}
        	return encrypt.ToString();
        }
    }
}