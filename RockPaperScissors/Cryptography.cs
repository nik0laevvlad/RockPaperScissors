using System;
using System.Security.Cryptography;
using System.Text;

namespace RockPaperScissors
{
    class Cryptography
    {
        private string secretKey;

        public void CalculateSecretKey()
        {
            byte[] byteArray = new byte[16];
            RNGCryptoServiceProvider.Create().GetBytes(byteArray);
            secretKey = BitConverter.ToString(byteArray).Replace("-", "");
        }

        public int GetComputerChoise(string[] arguments)
        {
            return RNGCryptoServiceProvider.GetInt32(arguments.Length);
        }

        public string GetHMAC(string word)
        {
            var hmac = new Org.BouncyCastle.Crypto.Macs.HMac(new Org.BouncyCastle.Crypto.Digests.Sha3Digest(256));
            hmac.Init(new Org.BouncyCastle.Crypto.Parameters.KeyParameter(Encoding.UTF8.GetBytes(secretKey)));
            byte[] result = new byte[hmac.GetMacSize()];
            byte[] bytes = Encoding.UTF8.GetBytes(word);

            hmac.BlockUpdate(bytes, 0, bytes.Length);
            hmac.DoFinal(result, 0);

            string hashString = BitConverter.ToString(result);
            hashString = hashString.Replace("-", "").ToLowerInvariant();

            return hashString;
        }

        public string GetSecretKey()
        {
            return secretKey;
        }
    }
}
