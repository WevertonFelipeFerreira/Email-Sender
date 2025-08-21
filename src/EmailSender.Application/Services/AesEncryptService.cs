using EmailSender.Application.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace EmailSender.Application.Services
{
    public class AesEncryptService : IPasswordProtector
    {
        private readonly string _key;
        public AesEncryptService(string key)
        {
            _key = key;
        }

        public string Encrypt(string plainText)
        {
            var key = Convert.FromBase64String(Convert.ToBase64String(Encoding.UTF8.GetBytes(_key)));
            using var aes = Aes.Create();
            aes.Key = key;
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor();
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            var result = new byte[aes.IV.Length + encryptedBytes.Length];
            Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length);
            Buffer.BlockCopy(encryptedBytes, 0, result, aes.IV.Length, encryptedBytes.Length);

            return Convert.ToBase64String(result);
        }

        public string Decrypt(string cypher)
        {
            var key = Convert.FromBase64String(Convert.ToBase64String(Encoding.UTF8.GetBytes(_key)));
            var fullCipher = Convert.FromBase64String(cypher);

            using var aes = Aes.Create();
            aes.Key = key;

            var iv = new byte[aes.BlockSize / 8];
            var cipherBytes = new byte[fullCipher.Length - iv.Length];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipherBytes, 0, cipherBytes.Length);
            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor();
            var plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}
