/*****************************************************************************
h e i n r i c h -h e r t z -b e r u f s k o l l e g  d e r  s t a d t  b o n n
Autor:				
Klasse:			
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
        static public bool RSA_Verschluesselung(string FileName,string ziel,string publicPath)
        {
            bool ergebnis = false;

            CspParameters cspp = new CspParameters();

            //Data to encrypt
            StreamReader SR = new StreamReader(FileName, Encoding.UTF7);
            string datainFile = SR.ReadToEnd();
            SR.Close();

            //public Key
            SR = new StreamReader(publicPath);
            cspp.KeyContainerName = "Key01";
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(cspp);
            string keytxt = SR.ReadToEnd();
            RSA.FromXmlString(keytxt);
            RSA.PersistKeyInCsp = true;
            if(RSA == null || RSA.KeySize <= 0 || keytxt.Length <= 0)
            {
                ergebnis = false;
            }
            else
            {
                ergebnis = true;
            }

            EncryptFile(FileName, ziel,RSA);

            return ergebnis;
        }

        static private void EncryptFile(string inFile,string outFile, RSACryptoServiceProvider RSA)
        {

            // Create instance of Aes for
            // symmetric encryption of the data.
            Aes aes = Aes.Create();
            ICryptoTransform transform = aes.CreateEncryptor();

            // Use RSACryptoServiceProvider to
            // encrypt the AES key.
            // rsa is previously instantiated:
            //    rsa = new RSACryptoServiceProvider(cspp);
            byte[] keyEncrypted = RSA.Encrypt(aes.Key, false);

            // Create byte arrays to contain
            // the length values of the key and IV.
            byte[] LenK = new byte[4];
            byte[] LenIV = new byte[4];

            int lKey = keyEncrypted.Length;
            LenK = BitConverter.GetBytes(lKey);
            int lIV = aes.IV.Length;
            LenIV = BitConverter.GetBytes(lIV);

            // Write the following to the FileStream
            // for the encrypted file (outFs):
            // - length of the key
            // - length of the IV
            // - ecrypted key
            // - the IV
            // - the encrypted cipher content

            using (FileStream outFs = new FileStream(outFile, FileMode.Create))
            {

                outFs.Write(LenK, 0, 4);
                outFs.Write(LenIV, 0, 4);
                outFs.Write(keyEncrypted, 0, lKey);
                outFs.Write(aes.IV, 0, lIV);

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
                    int blockSizeBytes = aes.BlockSize / 8;
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
        }
    }
}