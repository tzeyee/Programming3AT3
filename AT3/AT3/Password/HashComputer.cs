using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

//Tze Yee Hon P466426
// AT3 05/12/2019

namespace AT3.Password
{
    public class HashComputer
    {
        public string GetPasswordHashAndSalt(string message)
        {
            //use SHA256 to generate the hash saltedpassword
            SHA256 sha = new SHA256CryptoServiceProvider();
            byte[] databytes = Utility.GetBytes(message);
            byte[] resultBytes = sha.ComputeHash(databytes);

            return Utility.GetString(resultBytes);
        }
    }
}
