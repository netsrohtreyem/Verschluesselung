/*****************************************************************************
h e i n r i c h -h e r t z -b e r u f s k o l l e g  d e r  s t a d t  b o n n
Autor:			Meyer	
Klasse:			IA109
Datum:			22.1.2010
Datei:			Datei_verschluesseln.cpp
Einsatz:		Definition
Beschreibung:	Verschlüsselt die übergebene Datei und liefert den Pfad
				der verschlüsselten Datei zurück
******************************************************************************
Aenderungen:
28.5.2011		Auf ASCII 254 erweitert
17.6.2013       auf Binary umgestellt
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
        static string Datei_verschluesseln(ref string Quelle,ref string Schluessel)
        {
        	//lokale Daten
        	string Geheim = null;
        	string Auswahl = null;
        	string Pfad = null;
            string Eingabe = null;
        	int auswahl = 0;

            StringBuilder S_Schluessel = new StringBuilder(Schluessel);
        	//Datei auswaehlen
        	Console.WriteLine("\n\n");
        	Console.WriteLine("Geben Sie den Pfad zur Datei an:\n");
        	Pfad = Console.ReadLine();

            Quelle = File.ReadAllText(Pfad, Encoding.UTF7);
            if (Quelle != "")
            {
                Console.WriteLine("\n\nGeladen wurde: " + Pfad + "\n\n");
            }
            else
            {
                Console.WriteLine("\n\nDie Datei " + Pfad + " konnte nicht geladen werden!");
                return "";
            }
        	//Auswahlmenue
        	Auswahl = menue("Menue2.txt");
        	//Auswahl auswerten
        	auswahl = menue_auswerten(Auswahl);

           	switch(auswahl)
        	{
        		case 0:
        			break;
        		case 1: //Caesar
        			Console.WriteLine("\n\n");
        			Console.WriteLine("Geben Sie bitte den Schluessel ein: \n");
                    Eingabe = Console.ReadLine(); //Achtung keine verdeckte Eingabe!!!
                    Schluessel = Eingabe;
        			Geheim = Caesar_verschluesselung(Quelle,Quelle.Length,Eingabe);
        			//In Datei speichern
        			if(Geheim != null)
        			{
        				Console.WriteLine("\n\nVerschluesselung erfolgreich !\n\n");
        				Console.WriteLine("Geben Sie den Zielpfad an, an dem das Ergebnis gespeichert werden soll: \n");
        				Pfad = Console.ReadLine();

                        File.WriteAllText(Pfad, Geheim, Encoding.UTF7);

                        Console.WriteLine("\n\nDie verschluesselte Datei wurde gespeichert unter: ");
        				Console.WriteLine("\n"+Pfad+"\n");
        				Console.WriteLine("zurueck zum Hauptmenue mit einem beliebigen Tastendruck..\n");
        				while(!Console.KeyAvailable);
        			}
        			else
        			{
        				Console.WriteLine("\n\nVerschluesselung nicht erfolgreich !\n"); 
        			}
        			break;
        		case 2: //Vigenere
        			Console.WriteLine("\n\n");
        			Console.WriteLine("Geben Sie bitte den Schluessel ein: \n");
                    Eingabe = Console.ReadLine(); //Achtung keine verdeckte Eingabe!!!
                    Schluessel = Eingabe;
        			Geheim = Vigenere_verschluesselung(Quelle,Quelle.Length,Eingabe);
        			//In Datei speichern
        			if(Geheim != null)
        			{
        				Console.WriteLine("\n\nVerschluesselung erfolgreich !\n\n");
        				Console.WriteLine("Geben Sie den Zielpfad an, an dem das Ergebnis gespeichert werden soll: \n");
        				Pfad = Console.ReadLine();

                        File.WriteAllText(Pfad, Geheim, Encoding.UTF7);

                        Console.WriteLine("\n\nDie verschluesselte Datei wurde gespeichert unter: ");
        				Console.WriteLine("\n"+Pfad+"\n");
        				Console.WriteLine("zurueck zum Hauptmenue mit einem beliebigen Tastendruck..\n");
        				while(!Console.KeyAvailable);
        			}
        			else
        			{
        				Console.WriteLine("\n\nVerschluesselung nicht erfolgreich !\n"); 
        			}
        			break;
        		case 3: //OnetimePad
        			//Schlüssel generieren
        			S_Schluessel = new StringBuilder(Quelle,Quelle.Length);
        
        			//zufällige Werte zwischen 32 und 125 erzeugen
                    Random rand = new Random();
                    char u = '\0';
                    for (int index = 0; index < Quelle.Length; index++)
        			{
        				u = (char)(rand.Next(32,125));
        				S_Schluessel[index] = u; 
        			}
                    Schluessel = S_Schluessel.ToString();

        			Geheim = One_time_pad_verschluesselung(ref Quelle,Quelle.Length,ref Schluessel);
        			//In Datei speichern
        			if(Geheim != null)
        			{
        				Console.WriteLine("\n\nVerschluesselung erfolgreich !\n\n");
        				Console.WriteLine("Geben Sie den Zielpfad an, an dem das Ergebnis gespeichert werden soll: \n");
        				Pfad = Console.ReadLine();

                        File.WriteAllText(Pfad, Geheim, Encoding.UTF7);

                        File.WriteAllText("OTP_Schluessel.txt", Schluessel, Encoding.UTF7);

        				Console.WriteLine("\n\nDer Schluessel wurde gespeichert unter: ");
        				Console.WriteLine("\nSchluessel.txt\n");
                        Console.WriteLine("\nWeiter mit einem beliebigen Tastendruck...");
                        while (!Console.KeyAvailable) ;
      					return null;
        			}
        			else
        			{
        				Console.WriteLine("\n\nVerschluesselung nicht erfolgreich !\n"); 
        			}
        			break;
                case 4: //RSA
                    bool ergebnis = RSA_Verschluesselung(Pfad);
                    if (ergebnis)
                    {
                        Console.WriteLine("\n\nVerschluesselung erfolgreich !\n\n");
                    }
                    else
                    {
                        Console.WriteLine("\n\nVerschluesselung nicht erfolgreich !\n");
                    }
                    Console.WriteLine("\nWeiter mit einem beliebigen Tastendruck...");
                    while (!Console.KeyAvailable) ;
                    break;
        		case 5:
        			return null;
        		default:
        			break;
        	}

            return Geheim;
        }
    }
}