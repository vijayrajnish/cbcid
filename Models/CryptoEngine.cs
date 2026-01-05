using System.Security.Cryptography;
using System.Text;

namespace CBCID_APPLICATION.Models
{
    public static class CryptoEngine
    {
        public static string Encrypt(string input, string key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32).Substring(0, 32)); // 256-bit key
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                aes.GenerateIV();
                ICryptoTransform encryptor = aes.CreateEncryptor();

                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] encrypted = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);

                // Combine IV + encrypted data
                byte[] result = new byte[aes.IV.Length + encrypted.Length];
                Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length);
                Buffer.BlockCopy(encrypted, 0, result, aes.IV.Length, encrypted.Length);

                return Convert.ToBase64String(result);
            }
        }


        /// <summary>
        /// Method for decrypted the value  (use for Mine Mitra)
        /// </summary>
        /// <param name="input">holds value in string formate</param>
        /// <returns></returns>
        public static string Decrypt(string input, string key)
        {
            byte[] inputBytes = Convert.FromBase64String(input);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32).Substring(0, 32)); // 256-bit key
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                byte[] iv = new byte[aes.BlockSize / 8];
                byte[] encryptedData = new byte[inputBytes.Length - iv.Length];

                Buffer.BlockCopy(inputBytes, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(inputBytes, iv.Length, encryptedData, 0, encryptedData.Length);

                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor();
                byte[] decrypted = decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length);

                return Encoding.UTF8.GetString(decrypted);
            }
        }



    }
}
