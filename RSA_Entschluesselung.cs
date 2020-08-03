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
        static public bool RSA_Entschluesselung(ref string Quelle, string Pfad)
        {
            // Declare CspParmeters and RsaCryptoServiceProvider
            // objects with global scope of your Form class.
            CspParameters cspp = new CspParameters();
            RSACryptoServiceProvider rsa;

            // Public key file
            //const string PubKeyFile = @"rsaPublicKey.txt";

            // Key container name for
            // private/public key value pair.
            const string keyName = "Key01";            

            //Get private Key
            cspp.KeyContainerName = keyName;

            rsa = new RSACryptoServiceProvider(cspp);
            rsa.PersistKeyInCsp = true;

            if (rsa.PublicOnly == true)
            {
                Console.WriteLine("Key not set.");
                return false;
            }

            if (rsa == null)
            {
                Console.WriteLine("Key not set.");
                return false;
            }
            else
            {
               if (Pfad != null)
               {
                   // Create instance of Rijndael for
                   // symetric decryption of the data.
                   RijndaelManaged rjndl = new RijndaelManaged();
                   rjndl.KeySize = 256;
                   rjndl.BlockSize = 256;
                   rjndl.Mode = CipherMode.CBC;
                   rjndl.Padding = PaddingMode.None;

                   // Create byte arrays to get the length of
                   // the encrypted key and IV.
                   // These values were stored as 4 bytes each
                   // at the beginning of the encrypted package.
                   byte[] LenK = new byte[4];
                   byte[] LenIV = new byte[4];

                   // Consruct the file name for the decrypted file.
                   Console.WriteLine("Geben Sie den Pfad an, an dem das Ergebnis gespeichert werden soll: \n");
                   string outFile = Console.ReadLine();

                   // Use FileStream objects to read the encrypted
                   // file (inFs) and save the decrypted file (outFs).
                   using (FileStream inFs = new FileStream(Pfad, FileMode.Open))
                   {

                       inFs.Seek(0, SeekOrigin.Begin);
                       inFs.Seek(0, SeekOrigin.Begin);
                       inFs.Read(LenK, 0, 3);
                       inFs.Seek(4, SeekOrigin.Begin);
                       inFs.Read(LenIV, 0, 3);

                       // Convert the lengths to integer values.
                       int lenK = BitConverter.ToInt32(LenK, 0);
                       int lenIV = BitConverter.ToInt32(LenIV, 0);

                       // Determine the start postition of
                       // the ciphter text (startC)
                       // and its length(lenC).
                       int startC = lenK + lenIV + 8;
                       int lenC = (int)inFs.Length - startC;

                       // Create the byte arrays for
                       // the encrypted Rijndael key,
                       // the IV, and the cipher text.
                       byte[] KeyEncrypted = new byte[lenK];
                       byte[] IV = new byte[lenIV];

                       // Extract the key and IV
                       // starting from index 8
                       // after the length values.
                       inFs.Seek(8, SeekOrigin.Begin);
                       inFs.Read(KeyEncrypted, 0, lenK);
                       inFs.Seek(8 + lenK, SeekOrigin.Begin);
                       inFs.Read(IV, 0, lenIV);

                       // Use RSACryptoServiceProvider
                       // to decrypt the Rijndael key.
                       byte[] KeyDecrypted = null;

                       try
                       {
                           KeyDecrypted = rsa.Decrypt(KeyEncrypted, false);
                       }
                       catch (Exception ex)
                       {
                           Console.WriteLine(ex.Message);
                           return false;
                       }

                       // Decrypt the key.
                       ICryptoTransform transform = rjndl.CreateDecryptor(KeyDecrypted, IV);

                       // Decrypt the cipher text from
                       // from the FileSteam of the encrypted
                       // file (inFs) into the FileStream
                       // for the decrypted file (outFs).
                       using (FileStream outFs = new FileStream(outFile, FileMode.Create))
                       {

                           int count = 0;
                           int offset = 0;

                           // blockSizeBytes can be any arbitrary size.
                           int blockSizeBytes = rjndl.BlockSize / 8;
                           byte[] data = new byte[blockSizeBytes];


                           // By decrypting a chunk a time,
                           // you can save memory and
                           // accommodate large files.

                           // Start at the beginning
                           // of the cipher text.
                           inFs.Seek(startC, SeekOrigin.Begin);
                           using (CryptoStream outStreamDecrypted = new CryptoStream(outFs, transform, CryptoStreamMode.Write))
                           {
                               do
                               {
                                   count = inFs.Read(data, 0, blockSizeBytes);
                                   offset += count;
                                   outStreamDecrypted.Write(data, 0, count);
                               }
                               while (count > 0);

                               outStreamDecrypted.FlushFinalBlock();
                               outStreamDecrypted.Close();
                           }
                           outFs.Close();
                       }
                       inFs.Close();
                   }
                   try
                   {
                       Quelle = File.ReadAllText(Pfad, Encoding.UTF7);
                       return true;
                   }
                   catch (Exception)
                   {
                       return false;
                   }
               }
               else
               {
                   return false;
               }
            }
        }
    }
}