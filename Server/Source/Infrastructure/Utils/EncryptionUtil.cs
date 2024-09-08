using System.Security.Cryptography;
using System.Text;

namespace Torty.Web.Apps.ObsNightBotOverlay.Infrastructure.Utils
{
    public static class EncryptionUtil
    {
        /// <summary>
        /// Create a SHA256 hash of the <paramref name="value"/> provided.
        /// </summary>
        /// <param name="value">The value to encrypt via SHA256 algorithm</param>
        /// <param name="hashingVector">
        ///     An optional initialization vector to use in the SHA256 algorithm to increase entropy
        /// </param>
        /// <returns></returns>
        public static string SHA256Hash(string value, string hashingVector = null)
        {
            HMACSHA256 encryptor;
            if (string.IsNullOrWhiteSpace(hashingVector))
            {
                encryptor = new HMACSHA256();
            }
            else
            {
                byte[] hashingKeyBytes = Encoding.UTF8.GetBytes(hashingVector);
                encryptor = new HMACSHA256(hashingKeyBytes);
            }
            
            byte[] unencryptedBytes = Encoding.UTF8.GetBytes(value);
            byte[] encryptedBytes = encryptor.ComputeHash(unencryptedBytes);
            
            string hash = string.Empty;
            foreach (byte t in encryptedBytes)
                hash += t.ToString("X2");
            
            return hash;
        }
    }
}
