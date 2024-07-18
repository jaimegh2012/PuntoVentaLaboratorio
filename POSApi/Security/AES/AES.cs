

using POSApi.Enviroments.Keys;
using System.Security.Cryptography;
using System.Text;

namespace POSApi.Security.AES
{
    public class AES
    {
        public static string EncryptString(string plainText)
        {
            Llaves keys = new();

            byte[] array;
            var iv = Encoding.UTF8.GetBytes(keys.AES_VECTOR_INITIAL);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(keys.AES_KEY);
                aes.IV = iv;
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string DecryptString(string cipherText)
        {
            Llaves keys = new();

            var cipherTextReformado = cipherText.Replace(" ", "+");

            var iv = Encoding.UTF8.GetBytes(keys.AES_VECTOR_INITIAL);

            byte[] buffer = Convert.FromBase64String(cipherTextReformado);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(keys.AES_KEY);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }



    }
}
