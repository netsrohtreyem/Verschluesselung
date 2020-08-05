/*****************************************************************************
h e i n r i c h -h e r t z -b e r u f s k o l l e g  d e r  s t a d t  b o n n
Autor:			Meyer	
Klasse:			IA109
Datum:			22.1.2010
Datei:			RSA_Entschluesselung.cs
Einsatz:		Funktionsdefinition
Beschreibung:	Definiert die Funktion RSA_Entschluesselung()

******************************************************************************
Aenderungen:
5.5.2015    Beginn der Entwicklung
******************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Verschluesselung
{
    partial class main
    {
        static public void RSA_CreateKeyPair()
        {
            //Export the key information to an RSAParameters object.
            //Pass false to export the public key information or pass
            //true to export public and private key information.
            RSACryptoServiceProvider RSA;
            CspParameters cspp = new CspParameters();
            cspp.KeyContainerName = "Key01";
            RSA = new RSACryptoServiceProvider(cspp);
            RSA.PersistKeyInCsp = true;
            RSAParameters RSAParams = RSA.ExportParameters(true);
            StreamWriter sw = new StreamWriter("publicKey.key", false);
            sw.Write(RSA.ToXmlString(false));
            sw.Close();

            sw = new StreamWriter("KeyPair.key", false);
            sw.Write(RSA.ToXmlString(true));
            sw.Close();
        }
    }
}