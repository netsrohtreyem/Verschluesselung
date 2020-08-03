/*****************************************************************************
h e i n r i c h -h e r t z -b e r u f s k o l l e g  d e r  s t a d t  b o n n
Autor:			Meyer	
Klasse:			IA109
Datum:			22.1.2010
Datei:			One_time_pad_entschluesselung.cs
Einsatz:		Funktionsheader
Beschreibung:	Definiert die Funktion One_time_pad_entschluesselung()

******************************************************************************
Aenderungen:
05/06/2010	Ascii erweitert um ASCII 10/13 Zeilenumbruch CR/LF
29/05/2011  erweitert auf ASCII 254
 5.5.2015 Umbau auf C#
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
        static string One_time_pad_entschluesselung(ref string Quelle, int anzahl, ref string Schluessel)
        {
            StringBuilder encrypt = new StringBuilder(Quelle, anzahl); ;
            int index = 0;
            //Alle Zeichen entschlüsseln
            for (index = 0; index < anzahl-1; index++)
            {
                int temp = (int)Quelle[index] - (int)(Schluessel[index]);
                //wenn Überlauf, dann 255 hinzurechnen
                if (temp < 0)
                {
                    encrypt[index] = (char)(255 + (int)Quelle[index] - (int)Schluessel[index]);
                }
                else
                {
                    encrypt[index] = (char)((int)Quelle[index] - (int)Schluessel[index]);
                }
            }
            return encrypt.ToString();
        }
    }
}