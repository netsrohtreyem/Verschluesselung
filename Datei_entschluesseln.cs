/*****************************************************************************
h e i n r i c h -h e r t z -b e r u f s k o l l e g  d e r  s t a d t  b o n n
Autor:			Meyer	
Klasse:			IA109
Datum:			22.5.2010
Datei:			Datei_entschluesseln.cpp
Einsatz:		Definition
Beschreibung:	Entschlüsselt die übergebene Datei und liefert den Pfad
				der entschlüsselten Datei zurück
******************************************************************************
Aenderungen:
29/05/2011  erweitert auf ASCII 254
17.6.2013   auf Binary umgestellt
*****************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Verschluesselung
{
    partial class main
    {
        static string Datei_entschluesseln(ref string Quelle, ref string Schluessel)
        {
        	//lokale Daten
        	string Original = null;
        	string Auswahl = null;
        	string Pfad = null;
        	string Schluesseldatei = null;
            string Eingabe = null;
        	int auswahl = 0;


        	//Auswahlmenue
        	Auswahl = menue("Menue2.txt");
        	//Auswahl auswerten
        	auswahl = menue_auswerten(Auswahl);
        	switch(auswahl)
        	{
        		case 0:
        			break;
        		case 1: //Caesar
                        //Datei auswaehlen
                    Console.WriteLine("\n\n");
                    Console.WriteLine("Geben Sie den Pfad zur verschlüsselten Datei an:\n");
                    Pfad = Console.ReadLine();

                    try
                    {
                        Quelle = File.ReadAllText(Pfad, Encoding.UTF7);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n");
                    Console.WriteLine("Die folgende Datei wurde geladen: " + Pfad + "\n\n");

                    if (Quelle == "")
                        return "";
        			Console.WriteLine("\n\n");
        			Console.WriteLine("Geben Sie bitte den Schluessel ein: \n");
                    Eingabe = Console.ReadLine(); //Achtung keine verdeckte Eingabe!!!
                    Schluessel = Eingabe;
                    Original = Caesar_entschluesselung(Quelle,Quelle.Length,Eingabe);
        			//In Datei speichern
        			if(Original != null)
        			{
                        Console.WriteLine("\n\nEntschluesselung erfolgreich !\n\n");
                        Console.WriteLine("Geben Sie den Zielpfad an, an dem das Ergebnis gespeichert werden soll: \n");
                        Pfad = Console.ReadLine();

                        File.WriteAllText(Pfad, Original,Encoding.UTF8);

                        Console.WriteLine("\n\nDie entschluesselte Datei wurde gespeichert unter: ");
                        Console.WriteLine("\n" + Pfad + "\n");
                        Console.WriteLine("zurueck zum Hauptmenue mit einem beliebigen Tastendruck..\n");
                        while (!Console.KeyAvailable) ;
        			}
        			else
        			{
                        Console.WriteLine("\n\nEntschluesselung nicht erfolgreich !\n"); 
        			}
        			break;
        		case 2: //Vigenere
                        //Datei auswaehlen
                    Console.WriteLine("\n\n");
                    Console.WriteLine("Geben Sie den Pfad zur verschlüsselten Datei an:\n");
                    Pfad = Console.ReadLine();

                    try
                    {
                        Quelle = File.ReadAllText(Pfad, Encoding.UTF7);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n");
                    Console.WriteLine("Die folgende Datei wurde geladen: " + Pfad + "\n\n");

                    if (Quelle == "")
                        return "";
        			Console.WriteLine("\n\n");
        			Console.WriteLine("Geben Sie bitte den Schluessel ein: \n");
                    Eingabe = Console.ReadLine(); //Achtung keine verdeckte Eingabe!!!
                    Schluessel = Eingabe;
        			Original = Vigenere_entschluesselung(Quelle,Quelle.Length,Eingabe);
        			//In Datei speichern
        			if(Original != null)
        			{
                        Console.WriteLine("\n\nEntschluesselung erfolgreich !\n\n");
                        Console.WriteLine("Geben Sie den Zielpfad an, an dem das Ergebnis gespeichert werden soll: \n");
                        Pfad = Console.ReadLine();
                        
                        File.WriteAllText(Pfad, Original,Encoding.UTF8);

                        Console.WriteLine("\n\nDie entschluesselte Datei wurde gespeichert unter: ");
                        Console.WriteLine("\n" + Pfad + "\n");
                        Console.WriteLine("zurueck zum Hauptmenue mit einem beliebigen Tastendruck..\n");
                        while (!Console.KeyAvailable) ;
        			}
        			else
        			{
                        Console.WriteLine("\n\nEntschluesselung nicht erfolgreich !\n"); 
        			}
        			break;
        		case 3: //OnetimePad
                        //Datei auswaehlen
                    Console.WriteLine("\n\n");
                    Console.WriteLine("Geben Sie den Pfad zur verschlüsselten Datei an:\n");
                    Pfad = Console.ReadLine();

                    try
                    {
                        Quelle = File.ReadAllText(Pfad, Encoding.UTF7);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n");
                    Console.WriteLine("Die folgende Datei wurde geladen: " + Pfad + "\n\n");

                    if (Quelle == "")
                        return "";
        			//Schluesseldatei öffnen + Schluessel auslesen
        			Console.WriteLine("\n\nGeben Sie bitte den Pfad zur Schluessel-Datei ein:\n");
                    Eingabe = Console.ReadLine(); //Achtung keine verdeckte Eingabe!!!

                    try
                    {
                        Schluesseldatei =  File.ReadAllText(Eingabe,Encoding.UTF7);
                    }
                    catch(Exception)
                    {
        				Console.WriteLine("\nDie Schluessel-Datei kann nicht geoeffnet werden!\n");
        				Console.WriteLine("\nWeiter mit einem beliebigen Tastendruck...");
                        while (!Console.KeyAvailable) ;
        				return null;
                    }

                    Schluessel = Schluesseldatei;

        			Original = One_time_pad_entschluesselung(ref Quelle,Quelle.Length,ref Schluessel);
        			//In Datei speichern
        			if(Original != "")
        			{
                        Console.WriteLine("\n\nEntschluesselung erfolgreich !\n\n");
                        Console.WriteLine("Geben Sie den Zielpfad an, an dem das Ergebnis gespeichert werden soll: \n");
                        Pfad = Console.ReadLine();

                        File.WriteAllText(Pfad, Original,Encoding.UTF8);

                        Console.WriteLine("\n\nDie entschluesselte Datei wurde gespeichert unter: ");
                        Console.WriteLine("\n" + Pfad + "\n");
                        Console.WriteLine("zurueck zum Hauptmenue mit einem beliebigen Tastendruck..\n");
                        while (!Console.KeyAvailable) ;
        			}
        			else
        			{
                        Console.WriteLine("\n\nEntschluesselung nicht erfolgreich !\n"); 
        			}
        			break;
                case 4: //RSA
                    //Auswahlmenue
                    Auswahl = menue("Menue3.txt");
                    //Auswahl auswerten
                    auswahl = menue_auswerten(Auswahl);
                    switch (auswahl)
                    {
                        case 1: //entschluesseln, Keys vorhanden
                                //Datei auswaehlen
                            Console.WriteLine("\n\n");
                            Console.WriteLine("Geben Sie den Pfad zur verschlüsselten Datei an:\n");
                            Pfad = Console.ReadLine();
                            
                            Console.WriteLine("\n");
                            Console.WriteLine("Geben Sie bitte den Pfad zum private Key ein: \n");
                            string privatePath = Console.ReadLine(); 
                            if (!File.Exists(privatePath))
                            {
                                Console.WriteLine("\nprivate Key fehlt!");
                                return null;
                            }
                            else
                            { }
                            Console.WriteLine("\nGeben Sie den Zielpfad an, an dem das Ergebnis gespeichert werden soll: ");
                            string Zielpfad = Console.ReadLine();

                            bool ergebnis = RSA_Entschluesselung(ref Quelle,Pfad, privatePath, Zielpfad);
                            Original = Quelle;
                            if (ergebnis)
                            {
                                Console.WriteLine("\n\nEntschluesselung erfolgreich !\n\n");
                            }
                            else
                            {
                                Console.WriteLine("\n\nEntschluesselung nicht erfolgreich !\n");
                            }
                            Console.WriteLine("\nWeiter mit einem beliebigen Tastendruck...");
                            while (!Console.KeyAvailable) ;
                            break;
                        case 2: //Keys erzeugen und exportieren
                            RSA_CreateKeyPair();
                            Console.WriteLine("\nKeys erzeugt!");
                            Console.WriteLine("\nWeiter mit einem beliebigen Tastendruck...");
                            while (!Console.KeyAvailable) ;
                            Original = "";
                            break;
                        case 3:
                            return null;
                    }
                    break;
        		case 5:
        			return null;
        		default:
        			break;
        	}

        	return Original;
        }
    }
}