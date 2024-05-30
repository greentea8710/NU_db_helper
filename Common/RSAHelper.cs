using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Common
{
    public static class RSAHelper
    {
        // 生成RSA密钥对
        public static (string publicKey, string privateKey) GenerateKeys(int keySize = 2048)
        {
            using (var rsa = new RSACryptoServiceProvider(keySize))
            {
                return (rsa.ToXmlString(false), rsa.ToXmlString(true));
            }
        }

        // 加密方法
        public static byte[] Encrypt(string dataToEncrypt, string publicKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKey);

                var dataToEncryptBytes = Encoding.UTF8.GetBytes(dataToEncrypt);
                var encryptedData = rsa.Encrypt(dataToEncryptBytes, false); // 使用PKCS#1 v1.5填充

                return encryptedData;
            }

        }

        // 解密方法
        public static string Decrypt(byte[] dataToDecrypt, string privateKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKey);

                var decryptedDataBytes = rsa.Decrypt(dataToDecrypt, false); // 使用PKCS#1 v1.5填充
                var decryptedData = Encoding.UTF8.GetString(decryptedDataBytes);

                return decryptedData;
            }
        }
    }
}
