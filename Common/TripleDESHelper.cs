using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace Common
{
    public static class TripleDESHelper
    {
        public static byte[] Encrypt(string dataToEncrypt, string key, string iv)
        {
            using (TripleDES tripleDES = TripleDES.Create())
            {
                tripleDES.Key = Convert.FromBase64String(key);
                tripleDES.IV = Convert.FromBase64String(iv);
                tripleDES.Mode = CipherMode.CBC;
                tripleDES.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = tripleDES.CreateEncryptor();

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(dataToEncrypt);
                        }
                        return msEncrypt.ToArray();
                    }
                }
            }
        }

        public static string Decrypt(byte[] dataToDecrypt, string key, string iv)
        {
            using (TripleDES tripleDES = TripleDES.Create())
            {
                tripleDES.Key = Convert.FromBase64String(key);
                tripleDES.IV = Convert.FromBase64String(iv);
                tripleDES.Mode = CipherMode.CBC;
                tripleDES.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = tripleDES.CreateDecryptor();

                using (MemoryStream msDecrypt = new MemoryStream(dataToDecrypt))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        public static string GenerateKey()
        {
            using (TripleDES tripleDES = TripleDES.Create())
            {
                tripleDES.GenerateKey();
                return Convert.ToBase64String(tripleDES.Key);
            }
        }

        public static string GenerateIV()
        {
            using (TripleDES tripleDES = TripleDES.Create())
            {
                tripleDES.GenerateIV();
                return Convert.ToBase64String(tripleDES.IV);
            }
        }
    }
}
