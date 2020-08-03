/*****************************************************************************
h e i n r i c h -h e r t z -b e r u f s k o l l e g  d e r  s t a d t  b o n n
Autor:			Meyer	
Klasse:			IA109
Datum:			21.4.2015
Datei:			splashscreen.cs
Einsatz:		Funktionsdefinition
Beschreibung:	Definiert die Funktion splashscreen(), läd aus einer Datei die Splashinfos
Funktionen:		void splashscreen(string)
******************************************************************************
Aenderungen:
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
        static void splashscreen(string value)
        {
        	//lokale Daten
            string Infos = null;
        	string hilf;

        	//int anzahl = 0;
        	//char temp = '\0';
        
        	//Daten laden
        	FileStream fin = null;
            try
            {
                fin = new FileStream(value, FileMode.Open);
            }
            catch (Exception)
            {
            }

            StreamReader SR = new StreamReader(fin);
        	if(fin.CanRead)
        	{
                while ((hilf = SR.ReadLine())!= null)
                {
                    Infos += hilf + '\n';
                }
        	}
        	else
        	{
                SR.Close();
                fin.Close();
        		return;
        	}

            SR.Close();
            fin.Close();
            Console.Write(Infos);
        }
    }
}