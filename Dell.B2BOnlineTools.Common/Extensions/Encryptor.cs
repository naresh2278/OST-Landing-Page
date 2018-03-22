using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Dell.B2BOnlineTools.Common.Extensions
{
    public static class Encryptor
    {
        private static byte[] Key;
        private static byte[] IV;

        static Encryptor()
        {
            Key = new ASCIIEncoding().GetBytes("NeverGuessIt");
            IV = new ASCIIEncoding().GetBytes("must be 16 bytes");
        }

        public static string Encrypt(this string Value)
        {
            if (string.IsNullOrEmpty(Value)) return null;
            byte[] bytEncrypted;
            string strHexByte;
            StringBuilder sbOut = new StringBuilder();
            MemoryStream theStream = new MemoryStream();
            RijndaelManaged RMCrypto = new RijndaelManaged();
            CryptoStream CryptWrite = new CryptoStream(theStream, RMCrypto.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
            CryptWrite.Write(System.Text.Encoding.ASCII.GetBytes(Value), 0, Value.Length);
            CryptWrite.FlushFinalBlock();
            theStream.Seek(0, System.IO.SeekOrigin.Begin);
            bytEncrypted = new byte[theStream.Length];
            theStream.Read(bytEncrypted, 0, (int)theStream.Length);
            for (int y = 0; y < bytEncrypted.Length; y++)
            {
                strHexByte = bytEncrypted[y].ToString("X");
                if (strHexByte.Length == 1)
                    sbOut.Append("0" + strHexByte);
                else
                    sbOut.Append(strHexByte);
            }
            theStream.Close();
            CryptWrite.Close();
            return sbOut.ToString();
        }
        public static string Decrypt(this string Value)
        {
            if (string.IsNullOrEmpty(Value)) return null;
            byte[] bytDecrypted;
            string strDecrypted;
            TextReader SReader;
            bytDecrypted = new byte[Value.Length / 2];
            for (int x = 0; x < (Value.Length / 2); x++)
            {
                bytDecrypted[x] = byte.Parse(Value.Substring(x * 2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            MemoryStream theStream = new MemoryStream(bytDecrypted);
            RijndaelManaged RMCrypto = new RijndaelManaged();
            theStream.Seek(0, System.IO.SeekOrigin.Begin);
            CryptoStream CryptRead = new CryptoStream(theStream, RMCrypto.CreateDecryptor(Key, IV), CryptoStreamMode.Read);
            SReader = new StreamReader(CryptRead);
            strDecrypted = SReader.ReadToEnd();
            theStream.Close();
            CryptRead.Close();
            SReader.Close();
            return strDecrypted;
        }
    }
}
