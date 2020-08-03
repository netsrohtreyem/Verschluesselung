/*****************************************************************************
h e i n r i c h -h e r t z -b e r u f s k o l l e g  d e r  s t a d t  b o n n
Autor:			Meyer	
Klasse:			IA109
Datum:			22.1.2010
Datei:			RSA_Verschluesselung.cs
Einsatz:		Funktionsdefinition
Beschreibung:	Definiert die Funktion RSA_Verschluesselung()

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
using System.ComponentModel;

namespace Verschluesselung
{
    partial class main
    {
        static public bool RSA_Verschluesselung(string FileName)
        {
            //public Key laden/importiern evtl. Keys erzeugen
            CspParameters cspp = new CspParameters();
            RSACryptoServiceProvider rsa;

            // Public key file
            const string PubKeyFile = @"rsaPublicKey.txt";

            // Key container name for
            // private/public key value pair.
            const string keyName = "Key01";            

            //public Key holen
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(PubKeyFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            cspp.KeyContainerName = keyName;
            rsa = new RSACryptoServiceProvider(cspp);
            string keytxt = "";
            try
            {
                keytxt = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            rsa.FromXmlString(keytxt);
            rsa.PersistKeyInCsp = true;
            sr.Close();

            if (rsa == null)
            {
                Console.WriteLine("\nKey not set.\n");
                return false;
            }
            else
            {

                string fName = FileName;
                if (fName != null)
                {
                    FileInfo fInfo = new FileInfo(fName);
                    // Pass the file name without the path.
                    string inFile = fInfo.Name;
                    // Create instance of Rijndael for
                    // symetric encryption of the data.
                    RijndaelManaged rjndl = new RijndaelManaged();
                    rjndl.KeySize = 256;
                    rjndl.BlockSize = 256;
                    rjndl.Mode = CipherMode.CBC;
                    ICryptoTransform transform = rjndl.CreateEncryptor();

                    // Use RSACryptoServiceProvider to
                    // enrypt the Rijndael key.
                    byte[] keyEncrypted = rsa.Encrypt(rjndl.Key, false);

                    // Create byte arrays to contain
                    // the length values of the key and IV.
                    byte[] LenK = new byte[4];
                    byte[] LenIV = new byte[4];

                    int lKey = keyEncrypted.Length;
                    LenK = BitConverter.GetBytes(lKey);
                    int lIV = rjndl.IV.Length;
                    LenIV = BitConverter.GetBytes(lIV);


                    // Write the following to the FileStream
                    // for the encrypted file (outFs):
                    // - length of the key
                    // - length of the IV
                    // - ecrypted key
                    // - the IV
                    // - the encrypted cipher content

                    Console.WriteLine("Geben Sie den Zielpfad an, an dem das Ergebnis gespeichert werden soll: \n");
                    string outFile = Console.ReadLine();

                    using (FileStream outFs = new FileStream(outFile, FileMode.Create))
                    {

                        outFs.Write(LenK, 0, 4);
                        outFs.Write(LenIV, 0, 4);
                        outFs.Write(keyEncrypted, 0, lKey);
                        outFs.Write(rjndl.IV, 0, lIV);

                        // Now write the cipher text using
                        // a CryptoStream for encrypting.
                        using (CryptoStream outStreamEncrypted = new CryptoStream(outFs, transform, CryptoStreamMode.Write))
                        {

                            // By encrypting a chunk at
                            // a time, you can save memory
                            // and accommodate large files.
                            int count = 0;
                            int offset = 0;

                            // blockSizeBytes can be any arbitrary size.
                            int blockSizeBytes = rjndl.BlockSize / 8;
                            byte[] data = new byte[blockSizeBytes];
                            int bytesRead = 0;

                            using (FileStream inFs = new FileStream(inFile, FileMode.Open))
                            {
                                do
                                {
                                    count = inFs.Read(data, 0, blockSizeBytes);
                                    offset += count;
                                    outStreamEncrypted.Write(data, 0, count);
                                    bytesRead += blockSizeBytes;
                                }
                                while (count > 0);
                                inFs.Close();
                            }
                            outStreamEncrypted.FlushFinalBlock();
                            outStreamEncrypted.Close();
                        }
                        outFs.Close();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}