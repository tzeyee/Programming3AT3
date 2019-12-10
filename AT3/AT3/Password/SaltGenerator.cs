using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AT3.Password
{
    public static class SaltGenerator
    {
        private static RNGCryptoServiceProvider m_cryptoServiceProvider = null;
        private const int SALT_SIZE = 24;

        static SaltGenerator()
        {
            m_cryptoServiceProvider = new RNGCryptoServiceProvider();
        }

        public static string GetSaltString()
        {
            //to store salt byte
            byte[] saltBytes = new byte[SALT_SIZE];

            //generate salt in array
            m_cryptoServiceProvider.GetNonZeroBytes(saltBytes);

            //get string for salt
            string saltString = Utility.GetString(saltBytes);

            return saltString;
        }
    }
}
