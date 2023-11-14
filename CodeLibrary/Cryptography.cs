/**
* Copyright (c) 2013, Broadway
* All rights reserved.
* @author Yasir Ahmed <yasir@Broadway.pk>
* @version 1.0.1
*/

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.IO;
using System.Text;
using System.Web.Security;
using System.Security.Cryptography;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Jewar.CodeLibrary
{
    public class Cryptography
    {
        private static string word = "1$34)[+-@#";

        /// <summary>
        /// Function to encrypt a string
        /// </summary>
        /// <param name="plainMessage">String to be encrypted</param>
        /// <returns>Encrypted string</returns>
        public static string EncryptMessage(string plainMessage)
        {
            string result = string.Empty;
            try
            {
                string password = word;

                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                des.IV = new byte[8];
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, new byte[0]);
                des.Key = pdb.CryptDeriveKey("RC2", "MD5", 128, new byte[8]);
                MemoryStream ms = new MemoryStream(plainMessage.Length * 2);
                CryptoStream encStream = new CryptoStream(ms, des.CreateEncryptor(),
                    CryptoStreamMode.Write);
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainMessage);
                encStream.Write(plainBytes, 0, plainBytes.Length);
                encStream.FlushFinalBlock();
                byte[] encryptedBytes = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(encryptedBytes, 0, (int)ms.Length);
                encStream.Close();
                result = Convert.ToBase64String(encryptedBytes);
            }
            catch (Exception ex)
            {
                ExceptionHandling.AddSystemerrorlog("OvrLod.Cryptography.EncryptMessage:- " + ex.Message);
            }
            return result;
            
        }

        /// <summary>
        /// Function to decrypt an encrypted string
        /// </summary>
        /// <param name="encryptedBase64">Encrypted string</param>
        /// <returns>Decryted string</returns>
        public static string DecryptMessage(string encryptedBase64)
        {
            string result = string.Empty;
            try
            {
                string password = word;

                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                des.IV = new byte[8];
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, new byte[0]);
                des.Key = pdb.CryptDeriveKey("RC2", "MD5", 128, new byte[8]);
                byte[] encryptedBytes = Convert.FromBase64String(encryptedBase64);
                MemoryStream ms = new MemoryStream(encryptedBase64.Length);
                CryptoStream decStream = new CryptoStream(ms, des.CreateDecryptor(),
                    CryptoStreamMode.Write);
                decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                decStream.FlushFinalBlock();
                byte[] plainBytes = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(plainBytes, 0, (int)ms.Length);
                decStream.Close();
                result = Encoding.UTF8.GetString(plainBytes);
            }
            catch (Exception ex)
            {
                ExceptionHandling.AddSystemerrorlog("OvrLod.Cryptography.DecryptMessage:- " + ex.Message);
            }
            return result;
            
        }

        private const int keysize = 256;
        private static readonly byte[] initVectorBytes = Encoding.ASCII.GetBytes("tu89geji340t89u2");

        public static string Decrypt(string cipherText, string passPhrase)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null))
            {
                byte[] keyBytes = password.GetBytes(keysize / 8);
                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.Mode = CipherMode.CBC;
                    using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }
    }
}
